using MovieRentals_20020992.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MovieRentals_20020992.ViewModels
{
    public class MovieIndexViewModel
    {
        public int? ID { get; set; }
        public IPagedList<Movie> Movies { get; set; }
        public ICollection<Movie> topMovie { get; set; }
        public ICollection<Movie> trendingMovie { get; set; }
        public ICollection<Movie> newRelease { get; set; }
        public string Search { get; set; }
        public IEnumerable<GenreWithCount> GenresWithCount { get; set; }
        public string Genre { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }
        public IEnumerable<SelectListItem> GenreFilterItems
        {
            get
            {
                var allGenres = GenresWithCount.Select(gg => new SelectListItem
                {
                    Value = gg.GenreType,
                    Text = gg.GenreTypeWithCount
                });
                return allGenres;
            }
        }
    }
    public class GenreWithCount
    {
        public int MovieCount { get; set; }
        public string GenreType { get; set; }
        public string GenreTypeWithCount
        {
            get
            {
                return GenreType + " (" + MovieCount.ToString() + ")";
            }
        }
    }
}