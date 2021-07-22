﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Data.Models
{
    using static DataConstants;
    public class Movie
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

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


    }
}