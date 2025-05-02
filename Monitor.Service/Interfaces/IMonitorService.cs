namespace Monitor.Service.Interfaces
{
    public interface IMonitorService
    {
        Task ActivateMonitor(CancellationToken stopToken);
    }
}
