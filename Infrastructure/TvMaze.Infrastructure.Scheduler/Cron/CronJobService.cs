using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TvMaze.Infrastructure.Scheduler.Cron
{
    public abstract class CronJobService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly CrontabSchedule _expression;
        protected CronJobService(string cronExpression)
        {
            _expression = CrontabSchedule.Parse(cronExpression);
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTime.UtcNow);
            var delay = next - DateTimeOffset.Now;

            if (delay.TotalMilliseconds <= 0)
                await ScheduleJob(cancellationToken);

            _timer = new System.Timers.Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (sender, args) =>
            {
                _timer.Dispose();
                _timer = null;

                if (!cancellationToken.IsCancellationRequested)
                {
                    await ExecuteAsync(cancellationToken);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    await ScheduleJob(cancellationToken);
                }
            };

            _timer.Start();
            await Task.CompletedTask;
        }

        public abstract Task ExecuteAsync(CancellationToken cancellationToken);

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
