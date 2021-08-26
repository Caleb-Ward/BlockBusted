using MovieRentals_20020992.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.ViewModels
{
    public class CartViewModel
    {
        public List<CartLine> CartLines { get; set; }
        [Display(Name = "Cart Total")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCost { get; set; }

    }
}