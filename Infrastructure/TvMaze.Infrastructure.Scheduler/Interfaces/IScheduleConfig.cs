namespace TvMaze.Infrastructure.Scheduler.Interfaces
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
    }
}
