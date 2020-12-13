using Microsoft.Extensions.DependencyInjection;
using System;
using TvMaze.Infrastructure.Scheduler.Cron;
using TvMaze.Infrastructure.Scheduler.Interfaces;

namespace TvMaze.Infrastructure.Scheduler.Extensions
{
    public static class ScheduledServiceExtensions
    {
        public static void AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");

            var config = new ScheduleConfig<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
        }
    }
}
