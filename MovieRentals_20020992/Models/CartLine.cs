using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.Models
{
    public class CartLine
    {
        public int ID { get; set; }
        public string CartID { get; set; }
        public int MovieID { get; set; }
        [Range(0, 4, ErrorMessage = "Please enter an amount of weeks between 0 and 4")]
        public int RentTime { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Movie Movie { get; set; }
    }
}