using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Services.Movies
{
    public interface IMovieModel
    {
        string Title { get; }

        string Genre { get; }

        int YearReleased { get; }
    }
}
