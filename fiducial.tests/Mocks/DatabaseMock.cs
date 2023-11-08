using System.Data;
using fiducial.dal.Factories;
using Microsoft.Data.Sqlite;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace fiducial.tests.Mocks;

internal class DatabaseMock
{
    private readonly OrmLiteConnectionFactory dbFactory =
            new(":memory:", SqliteOrmLiteDialectProvider.Instance);

    public IDbConnection OpenConnection() => dbFactory.OpenDbConnection();

    public void Insert<T>(IEnumerable<T> items)
    {
        using var db = OpenConnection();
        db.CreateTableIfNotExists<T>();
        foreach (var item in items)
        {
            db.Insert(item);
        }
    }
}
