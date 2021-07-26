using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Home
{
    public class IndexViewModel
    {
        public int TotalMovies { get; init; }

        public int TotalUsers { get; init; }

        public List<MovieIndexViewModel> Movies { get; init; }
    }
}
