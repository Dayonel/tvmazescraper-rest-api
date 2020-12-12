using Microsoft.EntityFrameworkCore;

namespace TvMaze.Infrastructure.Data
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}
