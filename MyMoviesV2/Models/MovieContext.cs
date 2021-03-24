using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoviesV2.Models
{
    public class MovieContext : DbContext, IMovieContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
