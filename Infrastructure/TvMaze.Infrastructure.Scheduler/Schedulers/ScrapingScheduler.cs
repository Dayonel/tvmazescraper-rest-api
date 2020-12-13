using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Core.Interfaces.ServiceClients;
using TvMaze.Infrastructure.Scheduler.Cron;
using TvMaze.Infrastructure.Scheduler.Interfaces;
using TvMaze.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace TvMaze.Infrastructure.Scheduler.Schedulers
{
    public class ScrapingScheduler : CronJobService
    {
        private readonly ILogger<ScrapingScheduler> _logger;
        private readonly IServiceProvider _provider;
        public ScrapingScheduler(IScheduleConfig<ScrapingScheduler> config, ILogger<ScrapingScheduler> logger,
            IServiceProvider provider) 
            : base(config.CronExpression)
        {
            _logger = logger;
            _provider = provider;
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var scraperServiceClient = scope.ServiceProvider.GetRequiredService<IScraperServiceClient>();
                    await scraperServiceClient.ExecuteScraping();
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message());
            }
        }
    }
}
