using Monitor.Infrastructure.Data.AppContext.Models;

namespace Monitor.Domain.Interfaces
{
    public interface IMonitor
    {
        Task<List<App>> GetApps();
        Task ActiveMonitor(App app, CancellationToken cancellationToken);
    }
}