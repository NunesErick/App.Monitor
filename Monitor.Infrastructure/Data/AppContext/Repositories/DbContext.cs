using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Monitor.Infrastructure.Data.AppContext.Interfaces;
using Npgsql;

namespace Monitor.Infrastructure.Data.AppContext.Repositories
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _conf;
        private readonly string _connStringReadOnly, _connStringUpdate,_connStringOpen;
        private NpgsqlConnection _connectionReadOnly, _connectionUpdate;
        private SqlConnection _connectionOpen;
        private SemaphoreSlim _semaphore;
        private bool _disposed;

        public DbContext(IConfiguration conf)
        {
            _conf = conf;
            _connStringReadOnly = _conf.GetConnectionString("ReadOnlyConnection") ?? throw new Exception("Connection ReadOnly string not found");
            _connStringUpdate = _conf.GetConnectionString("UpdateConnection") ?? throw new Exception("Connection Update string not found");
            _connStringOpen = _conf.GetConnectionString("ConnectionOpen") ?? throw new Exception("Connection Update string not found");
            _semaphore = new SemaphoreSlim(1);
        }

        public NpgsqlConnection GetConnectionStringReadOnly()
        {
            if (_connectionReadOnly == null)
            {
                _connectionReadOnly = new NpgsqlConnection(_connStringReadOnly);
                _connectionReadOnly.Open();
            }
            return _connectionReadOnly;
        }

        public NpgsqlConnection GetSqlConnectionUpdate()
        {
            _semaphore.Wait();
            if (_connectionUpdate == null)
            {
                _connectionUpdate = new NpgsqlConnection(_connStringUpdate);
                _connectionUpdate.Open();
            }
            _semaphore.Release();
            return _connectionUpdate;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connectionReadOnly?.Close();
                    _connectionReadOnly?.Dispose();
                    _connectionUpdate?.Close();
                    _connectionUpdate?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public SqlConnection GetConnectionOpen()
        {
            _semaphore.Wait();
            if (_connectionOpen == null)
            {
                _connectionOpen = new SqlConnection(_connStringOpen);
                _connectionOpen.Open();
            }
            _semaphore.Release();
            return _connectionOpen;
        }
    }
}
