using Microsoft.Data.SqlClient;
using Npgsql;

namespace Monitor.Infrastructure.Data.AppContext.Interfaces
{
    public interface IDbContext
    {
        NpgsqlConnection GetConnectionStringReadOnly();
        NpgsqlConnection GetSqlConnectionUpdate();
        SqlConnection GetConnectionOpen();
    }
}
