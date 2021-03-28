using Microsoft.EntityFrameworkCore;
using MyMoviesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoviesV2.Repositories
{

    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _movieContext;

        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public async Task<Movie> Create(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            await _movieContext.SaveChangesAsync();

            return movie;
        }

        public async Task Delete(int Id)
        {
            var moveToDelete = _movieContext.Movies.FindAsync(Id);
            _movieContext.Movies.Remove(moveToDelete.Result);
            await _movieContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> Get()
        {
            return await _movieContext.Movies.ToListAsync();
        }

        public async Task<Movie> Get(int Id)
        {
            return await _movieContext.Movies.FindAsync(Id);
        }

        public Task Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
