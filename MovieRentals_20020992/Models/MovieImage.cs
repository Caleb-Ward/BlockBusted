using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.Models
{
    public class MovieImage
    {
        public int ID { get; set; }
        [Display(Name = "File")]
        [StringLength(100)]
        [Index(IsUnique = true)]

        public string FileName { get; set; }
        public virtual ICollection<MovieImageMapping> MovieImageMappings { get; set; }

    }
}