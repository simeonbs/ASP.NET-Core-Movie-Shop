using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Data.Models
{
    public class Genre
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();
    }
}
