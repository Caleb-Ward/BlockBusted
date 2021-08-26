using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.Models
{
    public class MovieImageMapping
    {
        public int ID { get; set; }
        public int ImageNumber { get; set; }
        public int MovieID { get; set; }
        public int MovieImageID { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual MovieImage MovieImage { get; set; }
    }

}