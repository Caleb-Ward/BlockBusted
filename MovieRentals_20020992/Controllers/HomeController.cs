using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals_20020992.Data;
using MovieRentals_20020992.Models;
using MovieRentals_20020992.ViewModels;
using System.Data.Entity;

namespace MovieRentals_20020992.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();
        public ActionResult Index()
        {
            //collect list of movies and count them
            MovieIndexViewModel viewModel = new MovieIndexViewModel();
            var movieList = db.Movies.Include(m => m.Genre);
            int movieNum = movieList.Count();

            //Get newest flim by release date
            var newRelease = movieList.OrderByDescending(m =>m.ReleaseYear).Take(1);

            //get random movie to use for main page, simulating a dynamic popularity system
            Random r = new Random();
            int randNum= 1;
            int randNum2 = 1;
            //stops duplicate movies displaying for the sections
            while (randNum2 == randNum || newRelease.Any(m => m.ID == randNum) || newRelease.Any(m => m.ID == randNum2))
            {
                randNum = r.Next(1, movieNum + 1);
                randNum2 = r.Next(1, movieNum + 1);
            }

            //get movies from id relating to generated numbers
            var topMovie = movieList.Where(m => m.ID.Equals(randNum));
            var trendingMovie = movieList.Where(m => m.ID.Equals(randNum2));

            viewModel.newRelease = newRelease.ToList();
            viewModel.topMovie = topMovie.ToList();
            viewModel.trendingMovie = trendingMovie.ToList();

            return View(viewModel);
        }

    }
}