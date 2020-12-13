using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TvMaze.Infrastructure.Data.Factories
{
    public class TvMazeDbContextFactory : IDesignTimeDbContextFactory<TvMazeDbContext>
    {
        public TvMazeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TvMazeDbContext>();

            builder.UseSqlite("DataSource=tvmaze.db");

            return new TvMazeDbContext(builder.Options);
        }
    }
}
