using Dapper;
using Monitor.Infrastructure.Data.AppContext.Interfaces;
using Monitor.Infrastructure.Data.Interfaces;
using Monitor.Infrastructure.DTO;

namespace Monitor.Infrastructure.Data.Repositories
{
    public class MonitorDAO : IMonitorDAO
    {
        private readonly IDbContext _dbContext;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public MonitorDAO(IDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<AppDTO>> GetApps()
        {
            const string query = "SELECT id,nome,link_interacao as LinkInteracao FROM aplicacoes";
            var conn = _dbContext.GetConnectionStringReadOnly();
            var retorno = conn.Query<AppDTO>(query).ToList();
            return retorno;            
        }
        public async Task InsertStatus(AppDTO app, int status)
        {
            const string query = "INSERT INTO log_apps (id_aplicacao, status) VALUES (@Id, @Status)";
            try
            {
                _semaphore.Wait();
                var conn = _dbContext.GetSqlConnectionUpdate();

                conn.Execute(query, new { Id = app.Id, Status = status });
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
