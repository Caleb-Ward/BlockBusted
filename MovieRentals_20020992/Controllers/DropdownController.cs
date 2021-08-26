using MovieRentals_20020992.Data;
using MovieRentals_20020992.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MovieRentals_20020992.Controllers
{
    public class DropdownController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: Dropdown
        public PartialViewResult GenreList()
        {
            DropdownViewModel viewModel = new DropdownViewModel();
            var genreList = db.Genres.OrderBy(m => m.Type);

            {
                viewModel.GenreList = genreList.ToList();
            }
            return PartialView(viewModel);
        }
    }
}