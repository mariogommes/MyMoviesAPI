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
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return movie; //Porque retorna movie? Boa pratica RestFul? Pesquisar.
        }

        public async Task Delete(int Id)
        {
            var MovieToDelete = await _context.Movies.FindAsync();
            _context.Movies.Remove(MovieToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> Get()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> Get(int Id)
        {
            return await _context.Movies.FindAsync();
        }

        public async Task Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
