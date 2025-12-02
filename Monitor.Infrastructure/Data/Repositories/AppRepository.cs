using Dapper;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure.Data.AppContext;
using Monitor.Infrastructure.Data.AppContext.Models;
using Monitor.Infrastructure.Data.Interfaces;

namespace Monitor.Infrastructure.Data.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _dbContext;

        public AppRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<App>> GetApps()
        {   
            return await _dbContext.Apps.ToListAsync();
        }
    }
}
