using TvMaze.Infrastructure.Scheduler.Interfaces;

namespace TvMaze.Infrastructure.Scheduler.Cron
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
    }
}
