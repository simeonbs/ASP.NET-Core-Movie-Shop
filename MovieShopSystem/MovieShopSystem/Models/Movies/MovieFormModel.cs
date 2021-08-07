using MovieShopSystem.Services.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Movies
{
    using static Data.DataConstants;
    public class MovieFormModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Title { get; init; }

        [Range(MovieYearMinLength, MovieYearMaxLength)]
        public int YearReleased { get; init; }

        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string Description { get; init; }

        [Required]
        [MinLength(5)]
        [MaxLength(DirectorMaxNameLength)]
        public string Director { get; init; }

        [Required]
        [MinLength(5)]
        [MaxLength(WriterMaxNameLength)]
        public string Writer { get; init; }
        
        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }

        public IEnumerable<MovieGenreServiceModel> Genres { get; set; }
    }
}
