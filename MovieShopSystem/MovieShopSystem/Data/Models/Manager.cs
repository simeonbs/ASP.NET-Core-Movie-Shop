using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieShopSystem.Data.Models
{
    public class Manager
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();
    }
}
