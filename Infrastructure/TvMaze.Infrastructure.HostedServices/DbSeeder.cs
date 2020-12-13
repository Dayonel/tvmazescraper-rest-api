using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Core.Interfaces.ServiceClients;
using TvMaze.Infrastructure.Data;

namespace TvMaze.Infrastructure.HostedServices
{
    public class DbSeeder : BackgroundService
    {
        private readonly IServiceProvider _provider;
        public DbSeeder(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Migrate();
            await Seed();
        }

        #region Migration
        private async Task Migrate()
        {
            using (var scope = _provider.CreateScope())
            {
                using (var _dbContext = scope.ServiceProvider.GetRequiredService<TvMazeDbContext>())
                {
                    await _dbContext.Database.MigrateAsync();
                    await _dbContext.Database.EnsureCreatedAsync();
                }
            }
        }
        #endregion

        #region Seed
        private async Task Seed()
        {
            using (var scope = _provider.CreateScope())
            {
                var scraperServiceClient = scope.ServiceProvider.GetRequiredService<IScraperServiceClient>();
                await scraperServiceClient.ExecuteScraping();
            }
        }
        #endregion
    }
}
