using MovieRentals_20020992.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.ViewModels
{
    public class CartSummaryViewModel
    {
        public int NumberOfWeeks { get; set; }
        public int NumberOfItems { get; set; }
        public ICollection<CartLine> CartItems { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCost { get; set; }

    }
}