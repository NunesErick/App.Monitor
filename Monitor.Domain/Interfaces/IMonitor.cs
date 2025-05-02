using Monitor.Infrastructure.DTO;

namespace Monitor.Domain.Interfaces
{
    public interface IMonitor
    {
        Task<List<AppDTO>> GetApps();
        Task ActiveMonitor(AppDTO app, CancellationToken cancellationToken);
    }
}