using System.Data;

namespace fiducial.dal.Factories;

/// <summary>
/// Abstraction over IDBConnection for unit testing
/// </summary>
public interface IDatabaseConnectionFactory
{
    IDbConnection GetConnection();
}