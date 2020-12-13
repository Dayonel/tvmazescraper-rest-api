using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Infrastructure.Data;
using TvMaze.Infrastructure.HostedServices;

namespace TvMaze.Infrastructure.DependencyBuilder
{
    public static class DependencyBuilder
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region DB
            services.AddDbContext<TvMazeDbContext>(options => options.UseSqlite(configuration.GetConnectionString(nameof(TvMazeDbContext))));
            #endregion

            #region Hosted services
            services.AddHostedService<DbSeeder>();
            #endregion
        }
    }
}
