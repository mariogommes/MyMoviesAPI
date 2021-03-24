using MyMoviesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoviesV2.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public Task<Movie> Create(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
