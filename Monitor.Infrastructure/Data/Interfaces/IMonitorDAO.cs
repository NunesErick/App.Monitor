using Monitor.Infrastructure.DTO;

namespace Monitor.Infrastructure.Data.Interfaces
{
    public interface IMonitorDAO
    {
        Task<List<AppDTO>> GetApps();
        Task InsertStatus(AppDTO app, int status);
    }
}