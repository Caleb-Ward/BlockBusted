using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRentals_20020992.Models
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "The movie title cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a movie title between 3 and 50 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-:;.,/?!&()\s]*$", ErrorMessage = "Please enter a movie title made up of only letters and spaces")]

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Movie description cannot be blank")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Please enter a movie description between 10 and 200 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-:;.,/?!&()\s]*$", ErrorMessage = "Please enter a movie description made up of only standard punctuation")]

        public string Description { get; set; }

        [Required(ErrorMessage = "The release year cannot be blank")]
        [Range(1800, 3000, ErrorMessage = "Please enter a year between 1800 and 3000")]

        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "The rental price cannot be blank")]
        [Range(0.10, 1000, ErrorMessage = "Please enter a rental price between 0.10 and 1000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The rating cannot be blank")]
        [Range(0, 5, ErrorMessage = "Please enter a rating between 1 and 5 (Or 0 for unrated)")]
        public int Rating { get; set; }
        public int? GenreID { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<MovieImageMapping> MovieImageMappings { get; set; }


    }
}