namespace Monitor.Domain.Interfaces
{
    public interface IMetrics
    {
        Task CollectSystemMetricsAsync();
    }
}
