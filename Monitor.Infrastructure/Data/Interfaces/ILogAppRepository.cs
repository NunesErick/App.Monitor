using Monitor.Infrastructure.Data.AppContext.Models;

namespace Monitor.Infrastructure.Data.Interfaces
{
    public interface ILogAppRepository
    {
        Task InsertLogAsync(LogApp log);
    }
}

