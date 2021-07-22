using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Movies
{
    public class AddMovieFormModel
    {
        public string Title { get; init; }

        public int YearReleased { get; init; }

        public string Description { get; init; }


        public string Director { get; init; }

        public string Writer { get; init; }
        
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Genre")]
        public int GenreId { get; init; }

        public IEnumerable<MovieGenreViewModel> Genres { get; set; }
    }
}
