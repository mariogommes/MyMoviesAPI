using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoviesV2.Models
{
    public interface IMovieContext
    {
        DbSet<Movie> Movies { get; set; }
    }
}
