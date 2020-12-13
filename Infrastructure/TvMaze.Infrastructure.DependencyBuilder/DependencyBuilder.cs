using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Core.Constants;
using TvMaze.Core.Entity;
using TvMaze.Core.Handlers;
using TvMaze.Core.Interfaces.Repository;
using TvMaze.Core.Interfaces.ServiceClients;
using TvMaze.Core.Interfaces.Services;
using TvMaze.Core.Services;
using TvMaze.Core.Settings;
using TvMaze.Infrastructure.Data;
using TvMaze.Infrastructure.Data.Repository;
using TvMaze.Infrastructure.HostedServices;
using TvMaze.Infrastructure.Scraper;
using TvMaze.Infrastructure.Scheduler.Extensions;
using TvMaze.Infrastructure.Scheduler.Schedulers;

namespace TvMaze.Infrastructure.DependencyBuilder
{
    public static class DependencyBuilder
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            #region Settings
            services.AddSingleton(configuration.BindSettings<PaginationSettings>(nameof(PaginationSettings)));
            services.AddSingleton(configuration.BindSettings<ScraperSettings>(nameof(ScraperSettings)));
            services.AddSingleton(configuration.BindSettings<RateLimitSettings>(nameof(RateLimitSettings)));
            services.AddSingleton(configuration.BindSettings<SchedulerSettings>(nameof(SchedulerSettings)));
            #endregion

            #region Http client
            services.AddSingleton<ThrottlingDelegateHandler>();
            services.AddHttpClient(HttpClientConstants.THROTTLED_HTTP_CLIENT)
                    .AddHttpMessageHandler<ThrottlingDelegateHandler>();
            #endregion

            #region DB
            services.AddDbContext<TvMazeDbContext>(options => options.UseSqlite(configuration.GetConnectionString(nameof(TvMazeDbContext))));
            #endregion

            #region Services
            services.AddTransient<IShowService, ShowService>();
            #endregion

            #region ServiceClients
            services.AddTransient<IScraperServiceClient, ScraperServiceClient>();
            #endregion

            #region Repositories
            services.AddTransient<IShowRepository<Show>, ShowRepository>();
            services.AddTransient<ICastRepository<Cast>, CastRepository>();
            #endregion

            #region Hosted services
            services.AddHostedService<DbSeeder>();
            #endregion

            #region Schedulers
            services.AddCronJob<ScrapingScheduler>(c => c.CronExpression = configuration.BindSettings<SchedulerSettings>(nameof(SchedulerSettings)).CronExpressionRecurrence);
            #endregion
        }
    }
}
