using Dapper;
using fiducial.dal.Factories;

namespace fiducial.dal.Seeds;

public class CreateTables
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

    public CreateTables(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    /// <summary>
    /// Creates core tables if they don't exist
    /// </summary>
    /// <returns></returns>
    public async Task Init()
    {
        using var connection = _databaseConnectionFactory.GetConnection();
        await _initBooks();

        async Task _initBooks()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                Book (
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