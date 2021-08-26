using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MovieRentals_20020992.Data;
using MovieRentals_20020992.Models;
using MovieRentals_20020992.ViewModels;
using PagedList;

namespace MovieRentals_20020992.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index(string genre, string search, string sortBy, int? page)
        {
            MovieIndexViewModel viewModel = new MovieIndexViewModel();
            var movies = db.Movies.Include(m => m.Genre);

            //find the movies where either the movie title, description, release year, or genre contains search
            if (!String.IsNullOrEmpty(search))
            {
                movies = movies.Where(m => m.Title.Contains(search) ||
                m.Description.Contains(search) ||
                m.Genre.Type.Contains(search) ||
                m.ReleaseYear.ToString().Contains(search));
                viewModel.Search = search;
            }

            //group search results into genres and count how many items in each genre
            viewModel.GenresWithCount = from matchingMovies in movies
                                        where
                                        matchingMovies.GenreID != null
                                        group matchingMovies by
                                        matchingMovies.Genre.Type into
                                        genreGroup
                                        select new GenreWithCount()
                                        {
                                            GenreType = genreGroup.Key,
                                            MovieCount = genreGroup.Count()
                                        };

            if (!String.IsNullOrEmpty(genre))
            {
                movies = movies.Where(m => m.Genre.Type == genre);
            }

            //sort the results
            switch (sortBy)
            {
                case "A-Z":
                    movies = movies.OrderBy(m => m.Title);
                    break;
                case "Z-A":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;

                case "price_lowest":
                    movies = movies.OrderBy(m => m.Price);
                    break;
                case "price_highest":
                    movies = movies.OrderByDescending(m => m.Price);
                    break;

                case "release_new":
                    movies = movies.OrderByDescending(m => m.ReleaseYear);
                    break;
                case "release_old":
                    movies = movies.OrderBy(m => m.ReleaseYear);
                    break;
                case "rating_high":
                    movies = movies.OrderByDescending(m => m.Rating);
                    break;
                case "rating_low":
                    movies = movies.OrderBy(m => m.Rating);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }
            int currentPage = (page ?? 1);
            viewModel.Movies = movies.ToPagedList(currentPage, Constants.PagedItems);

            viewModel.SortBy = sortBy;
            viewModel.Sorts = new Dictionary<string, string>
            {
                {"Price low to high", "price_lowest" },
                {"Price high to low", "price_highest" },
                {"A-Z", "A-Z" },
                {"Z-A", "Z-A" },
                {"Oldest", "release_old" },
                {"Newest", "release_new" },
                {"Highest rating", "rating_high" },
                {"Lowest rating", "rating_low" }
            };

            return View(viewModel);
        }


        // GET: Movies/Details/5
        [AllowAnonymous]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            MovieViewModel viewModel = new MovieViewModel();
            viewModel.GenreList = new SelectList(db.Genres, "ID", "Type");
            viewModel.ImageLists = new List<SelectList>();
            for (int i = 0; i < Constants.NumberOfMovieImages; i++)
            {
                viewModel.ImageLists.Add(new SelectList(db.MovieImages, "ID", "FileName"));
            }
            return View(viewModel);

        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel viewModel)
        {
            Movie movie = new Movie();
            movie.Title = viewModel.Title;
            movie.Description = viewModel.Description;
            movie.Price = viewModel.Price;
            movie.ReleaseYear = viewModel.ReleaseYear;
            movie.GenreID = viewModel.GenreID;
            movie.Rating = viewModel.Rating;
            movie.MovieImageMappings = new List<MovieImageMapping>();
            //get a list of selected Images without any blanks
            string[] movieImages = viewModel.MovieImages.Where(pi => !string.IsNullOrEmpty(pi)).ToArray();
            for (int i = 0; i < movieImages.Length; i++)
            {
                movie.MovieImageMappings.Add(new MovieImageMapping
                {
                    MovieImage = db.MovieImages.Find(int.Parse(movieImages[i])),
                    ImageNumber = i
                });
            }

            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.GenreList = new SelectList(db.Genres, "ID", "Type", movie.GenreID);
            viewModel.ImageLists = new List<SelectList>();
            for (int i = 0; i < Constants.NumberOfMovieImages; i++)
            {
                viewModel.ImageLists.Add(new SelectList(db.MovieImages, "ID", "FileName", viewModel.MovieImages[i]));
            }
            return View(viewModel);
        }


        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            MovieViewModel viewModel = new MovieViewModel();
            viewModel.GenreList = new SelectList(db.Genres, "ID", "Type", movie.GenreID);
            viewModel.ImageLists = new List<SelectList>();
            viewModel.GenreID = movie.Genre.ID;
            foreach (var imageMapping in movie.MovieImageMappings.OrderBy(pim => pim.ImageNumber))
            {
                viewModel.ImageLists.Add(new SelectList(db.MovieImages, "ID", "FileName",
                imageMapping.MovieImageID));
            }
            for (int i = viewModel.ImageLists.Count; i < Constants.NumberOfMovieImages; i++)
            {
                viewModel.ImageLists.Add(new SelectList(db.MovieImages, "ID", "FileName"));
            }
            viewModel.ID = movie.ID;
            viewModel.Title = movie.Title;
            viewModel.Description = movie.Description;
            viewModel.Price = movie.Price;
            viewModel.ReleaseYear = movie.ReleaseYear;
            viewModel.Rating = movie.Rating;
            return View(viewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieViewModel viewModel)
        {
            var movieToUpdate = db.Movies.Include(m => m.MovieImageMappings).Where(m => m.ID == viewModel.ID).Single();
            if (TryUpdateModel(movieToUpdate, "", new string[] { "Title", "Description", "Price", "ReleaseYear", "GenreID","Rating" }))
            {
                if (movieToUpdate.MovieImageMappings == null)
                {
                    movieToUpdate.MovieImageMappings = new List<MovieImageMapping>();
                }
                //get a list of selected Images without any Blanks
                string[] movieImages = viewModel.MovieImages.Where(pi => !string.IsNullOrEmpty(pi)).ToArray();
                for (int i = 0; i < movieImages.Length; i++)
                {
                    //get the image currently stored?
                    var imageMappingToEdit = movieToUpdate.MovieImageMappings.Where(pim => pim.ImageNumber == i).FirstOrDefault();
                    //find the new image
                    var image = db.MovieImages.Find(int.Parse(movieImages[i]));
                    //if there is nothing stored then we need to add new mapping
                    if (imageMappingToEdit == null)
                    {
                        //add image to the imagemappings
                        movieToUpdate.MovieImageMappings.Add(new MovieImageMapping
                        {
                            ImageNumber = i,
                            MovieImage = image,
                            MovieImageID = image.ID
                        });
                    }
                    //else its not a new file so edit the current mapping
                    else
                    {
                        //if they are not the same
                        if (imageMappingToEdit.MovieImageID != int.Parse(movieImages[i]))
                        {
                            //assign image property of the image mapping

                            imageMappingToEdit.MovieImage = image;
                        }
                    }
                }
                //delete any other imagemappings that the user did not include in their selections for the product
                for (int i = movieImages.Length; i < Constants.NumberOfMovieImages; i++)
                {
                    var imageMappingToEdit = movieToUpdate.MovieImageMappings.Where(pim => pim.ImageNumber == i).FirstOrDefault();
                    //if there is something stored in the mapping
                    if (imageMappingToEdit != null)
                    {
                        //delete the record form the mapping table directly
                        //just calling productToUpdate.ProductImageMappings.Remove(imageMappingToEdit)
                        //results in a FK error
                        db.MovieImageMappings.Remove(imageMappingToEdit);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }


        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            var orderLines = db.OrderLines.Where(ol => ol.MovieID == id);
            foreach (var ol in orderLines)
            {
                ol.MovieID = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }   
}
