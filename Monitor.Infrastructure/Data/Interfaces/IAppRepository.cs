using Monitor.Infrastructure.Data.AppContext.Models;

namespace Monitor.Infrastructure.Data.Interfaces
{
    public interface IAppRepository
    {
        Task<List<App>> GetApps();
    }
}