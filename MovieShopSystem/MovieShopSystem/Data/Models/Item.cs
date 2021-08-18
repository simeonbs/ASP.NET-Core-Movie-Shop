using System.ComponentModel.DataAnnotations;

namespace MovieShopSystem.Data.Models
{
    public class Item
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
