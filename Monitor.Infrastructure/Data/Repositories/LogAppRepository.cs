using Monitor.Infrastructure.Data.AppContext;
using Monitor.Infrastructure.Data.AppContext.Models;
using Monitor.Infrastructure.Data.Interfaces;
using System.Threading.Tasks;

namespace Monitor.Infrastructure.Data.Repositories
{
    public class LogAppRepository : ILogAppRepository
    {
        private readonly AppDbContext _dbContext;

        public LogAppRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertLogAsync(LogApp log)
        {
            _dbContext.LogApp.Add(log);
            await _dbContext.SaveChangesAsync();
        }
    }
}

