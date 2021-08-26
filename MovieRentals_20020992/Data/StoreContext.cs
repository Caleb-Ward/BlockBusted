using MovieRentals_20020992.Models;
using System.Data.Entity;

namespace MovieRentals_20020992.Data
{
    public class StoreContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public StoreContext() : base("name=StoreContext")
        {
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieImage> MovieImages { get; set; }
        public DbSet<MovieImageMapping> MovieImageMappings { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    
    }
}
