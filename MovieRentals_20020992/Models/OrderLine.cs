using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.Models
{
    public class OrderLine
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int? MovieID { get; set; }
        public int RentTime { get; set; }
        public string MovieTitle { get; set; }
        [Display(Name = "Weekly Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal WeeklyPrice { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Order Order { get; set; }
    }
}