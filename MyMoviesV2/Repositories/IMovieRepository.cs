using MyMoviesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoviesV2.Repositories
{
    interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Get();
        Task<Movie> Get(int Id);
        Task<Movie> Update(Movie movie);
        Task Create(Movie movie);
        Task Delete(int Id);
    }
}
