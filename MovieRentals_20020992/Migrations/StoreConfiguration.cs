namespace MovieRentals_20020992.Migrations.StoreConfiguration
{
    using Models;
    using System.Collections.Generic;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class StoreConfiguration : DbMigrationsConfiguration<MovieRentals_20020992.Data.StoreContext>
    {
        public StoreConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MovieRentals_20020992.Data.StoreContext";
        }

        protected override void Seed(MovieRentals_20020992.Data.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var genres = new List<Genre>
            {
                new Genre { Type = "Comedy" },
                new Genre { Type = "Action" },
                new Genre { Type = "Documentary" },
                new Genre { Type = "Drama" },
                new Genre { Type = "Child Friendly" },
                new Genre { Type = "Fantasy" },
                new Genre { Type = "Thriller" },
            };

            genres.ForEach(g => context.Genres.AddOrUpdate(m => m.Type, g));
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "American Pie",
                    Description = "In a bid to end their misfortune with women, four friends, Jim, Oz, Finch and Kevin, try every trick in the book to ensure that they lose their virginity before prom night.",
                    Price = 6.90M,
                    ReleaseYear = 1999,
                    Rating = 3,
                    GenreID = genres.Single(g => g.Type == "Comedy").ID
                },

                new Movie
                {
                    Title = "Die Hard",
                    Description = "Die Hard follows New York City police detective John McClane who is caught up in a terrorist takeover of a Los Angeles skyscraper while visiting his estranged wife.",
                    Price = 4.99M,
                    ReleaseYear = 1988,
                    Rating = 4,
                    GenreID = genres.Single(g => g.Type == "Action").ID
                },

                new Movie
                {
                    Title = "Planet Earth",
                    Description = "Dazzling, state-of-the-art high-definition imagery highlights this breathtaking documentary series featuring footage of some of the world's most awe-inspiring natural wonders.",
                    Price = 9.99M,
                    ReleaseYear = 2006,
                    Rating = 5,
                    GenreID = genres.Single(g => g.Type == "Documentary").ID
                },
            };

            movies.ForEach(g => context.Movies.AddOrUpdate(m => m.Title, g));
            context.SaveChanges();

            var orders = new List<Order>
             {
             new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street",
            Town="Town1",
             Country="Country", PostCode="PostCode" }, TotalPrice=631,
             UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 1) ,
             DeliveryName="Admin" },
             new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street",
            Town="Town1",
             Country="Country", PostCode="PostCode" }, TotalPrice=239,
             UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 2) ,
             DeliveryName="Admin" },
             new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street",
            Town="Town1",
             Country="Country", PostCode="PostCode" }, TotalPrice=239,
             UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 3) ,
             DeliveryName="Admin" },
             new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street",
            Town="Town1",
             Country="County", PostCode="PostCode" }, TotalPrice=631,
             UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 4) ,
             DeliveryName="Admin" },
             new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street",
            Town="Town1",
             Country="Country", PostCode="PostCode" }, TotalPrice=19.49M,
             UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 5) ,
             DeliveryName="Admin" }
             };

             orders.ForEach(g=> context.Orders.AddOrUpdate(o => o.DateCreated, g));
                        context.SaveChanges();
                        var orderLines = new List<OrderLine>
             {
             new OrderLine { OrderID = 1, MovieID = movies.Single( g=> g.Title ==
            "American Pie").ID,
             MovieTitle ="American Pie", RentTime =1, WeeklyPrice=movies.Single( g=>
            g.Title == "American Pie").Price },
             new OrderLine { OrderID = 2, MovieID = movies.Single( g=> g.Title ==
            "Planet Earth").ID,
             MovieTitle ="Planet Earth", RentTime =2, WeeklyPrice=movies.Single( g=>
            g.Title == "Planet Earth").Price },
             new OrderLine { OrderID = 3, MovieID = movies.Single( g=> g.Title ==
            "Die Hard").ID,
             MovieTitle ="Die Hard", RentTime =1, WeeklyPrice=movies.Single( g=>
            g.Title == "Die Hard").Price } };
            orderLines.ForEach(g => context.OrderLines.AddOrUpdate(ol => ol.OrderID, g));
            context.SaveChanges();

        }
    }
}
