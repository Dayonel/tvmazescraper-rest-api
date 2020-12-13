using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;
using TvMaze.Core.Interfaces.Services;
using TvMaze.Core.Services;
using TvMaze.Core.Settings;
using TvMaze.Infrastructure.Data;
using TvMaze.Infrastructure.Data.Repository;
using TvMaze.Infrastructure.HostedServices;

namespace TvMaze.Infrastructure.DependencyBuilder
{
    public static class DependencyBuilder
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Settings
            services.AddSingleton(configuration.BindSettings<PaginationSettings>(nameof(PaginationSettings)));
            #endregion

            #region DB
            services.AddDbContext<TvMazeDbContext>(options => options.UseSqlite(configuration.GetConnectionString(nameof(TvMazeDbContext))));
            #endregion

            #region Services
            services.AddTransient<IShowService, ShowService>();
            #endregion

            #region Repositories
            services.AddTransient<IShowRepository<Show>, ShowRepository>();
            #endregion

            #region Hosted services
            services.AddHostedService<DbSeeder>();
            #endregion
        }
    }
}
