using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRentals_20020992.Models
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "The genre type cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a genre between 3 and 50 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-:;.,/?!&()\s]*$", ErrorMessage = "Please enter a genre beginning with a capital letter and enter only letters and spaces.")]

        [Display(Name = "Genre")]
        public string Type { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}