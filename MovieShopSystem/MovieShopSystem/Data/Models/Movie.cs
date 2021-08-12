using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Data.Models
{
    using static DataConstants;
    public class Movie
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(MovieYearMinLength, MovieYearMaxLength)]
        public int YearReleased { get; set; }

        [Required]
        [Range(MovieDescMinLength, MovieDescMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(DirectorMaxNameLength)]
        public string Director { get; set; }

        [Required]
        [MaxLength(WriterMaxNameLength)]
        public string Writer { get; set; }

        public string ImageUrl { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; init; }

        public int ManagerId { get; init; }

        public Manager Manager { get; init; }

    }
}
