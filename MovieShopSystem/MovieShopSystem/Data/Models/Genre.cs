using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieShopSystem.Data.Models
{
    public class Genre
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();
    }
}
