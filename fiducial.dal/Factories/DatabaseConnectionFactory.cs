using System.Data;
using fiducial.dal.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace fiducial.dal.Factories;

public class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    protected readonly ConnectionStrings _connectionStrings;

    public DatabaseConnectionFactory(IOptions<ConnectionStrings> connectionStrings)
    {
        _connectionStrings = connectionStrings.Value;
    }
    
    public IDbConnection GetConnection()
    {
        return new SqliteConnection(_connectionStrings.LibraryDatabase);
    }
}