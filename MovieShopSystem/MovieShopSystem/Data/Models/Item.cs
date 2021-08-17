using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
