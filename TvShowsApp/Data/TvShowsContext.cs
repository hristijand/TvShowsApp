using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TvShowsApp.Models;

namespace TvShowsApp.Models
{
    public class TvShowsContext : DbContext
    {
        public TvShowsContext (DbContextOptions<TvShowsContext> options)
            : base(options)
        {
        }

        public DbSet<TvShow> TvShow { get; set; }

        public DbSet<Actors> Actors { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<RentedMovies> RentedMovies { get; set; }
    }
}
