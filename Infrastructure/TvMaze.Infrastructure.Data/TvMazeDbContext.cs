using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Entity;
using TvMaze.Infrastructure.Data.DbMapping;

namespace TvMaze.Infrastructure.Data
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Show> Show { get; set; }
        public DbSet<Cast> Cast { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.MapShows();
            builder.MapCasts();
        }
    }
}
