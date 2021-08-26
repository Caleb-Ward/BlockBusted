using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieRentals_20020992
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MoviesCreate",
                url: "Movies/Create",
                defaults: new { controller = "Movies", action = "Create" }
                );

              routes.MapRoute(
              name: "MoviesByGenreByPage",
              url: "Movies/{genre}/Page{page}",
              defaults: new { controller = "Movies", action = "Index" }
              );


            routes.MapRoute(
               name: "MoviesByPage",
               url: "Movies/Page{page}",
               defaults: new { controller = "Movies", action = "Index" }
               );

            routes.MapRoute(
                name: "MoviesByGenre",
                url: "Movies/{genre}",
                defaults: new { controller = "Movies", action = "Index" }
                );

            routes.MapRoute(
              name: "MoviesIndex",
              url: "Movies",
              defaults: new { controller = "Movies", action = "Index" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
