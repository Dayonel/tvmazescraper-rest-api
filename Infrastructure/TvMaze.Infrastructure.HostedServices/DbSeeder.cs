using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
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
    }
}
