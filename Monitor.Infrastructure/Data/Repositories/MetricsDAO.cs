using Monitor.Infrastructure.Data.AppContext.Interfaces;
using Monitor.Infrastructure.Data.Interfaces;
using Monitor.Infrastructure.DTO;

namespace Monitor.Infrastructure.Data.Repositories
{
    public class MetricsDAO : IMetricsDAO
    {
        private readonly IDbContext _conn;
        public MetricsDAO(IDbContext dbContext)
        {
            _conn = dbContext;
        }
       
        public Task<ViagemDTO> FetchLast10TripsAsync()
        {
            const string query = "SELECT cdViag, idLog, dtEntrada, dtGerada FROM viagem ORDER BY dtEntrada DESC LIMIT 10";
            var conn = _conn.GetConnectionOpen();
            throw new NotImplementedException();
        }

        public Task<DateTime> GetDateGenerated()
        {
            throw new NotImplementedException();
        }
    }
}
