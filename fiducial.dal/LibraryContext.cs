namespace fiducial.dal;

using System.Data;
using Dapper;
using fiducial.dal.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

public class LibraryContext
{
    protected readonly ConnectionStrings _connectionStrings;

    public LibraryContext(IOptions<ConnectionStrings> connectionStrings)
    {
        _connectionStrings = connectionStrings.Value;
    }

    /// <summary>
    /// Initiates the connection to database
    /// </summary>
    /// <returns>IDbConnection</returns>
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionStrings.LibraryDatabase);
    }

    /// <summary>
    /// Creates core tables if they don't exist
    /// </summary>
    /// <returns></returns>
    public async Task Init()
    {
        using var connection = CreateConnection();
        await _initBooks();

        async Task _initBooks()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                Books (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Title TEXT,
                    Author TEXT,
                    IsBorrowed BIT NOT NULL DEFAULT 0
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}