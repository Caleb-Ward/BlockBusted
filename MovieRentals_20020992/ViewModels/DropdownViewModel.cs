using MovieRentals_20020992.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MovieRentals_20020992.ViewModels
{
    public class DropdownViewModel
    {
        public ICollection<Genre> GenreList { get; set; }
    }

}