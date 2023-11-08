namespace fiducial.dal.Repositories.Book;

using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using fiducial.dal.Factories;
using fiducial.dal.Models;

public class BookRepository : IBookRepository
{
    private readonly IDatabaseConnectionFactory _dbFactory;

    public BookRepository(IDatabaseConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<Book>> List(string query)
    {
        using var connection = _dbFactory.GetConnection();
        var sql = """
            SELECT * FROM Book
            WHERE (@query IS NULL OR @query = '') OR (Title = @query OR Author = @query)
        """;
        return await connection.QueryAsync<Book>(sql, new {query});
    }

    public async Task<Book> GetById(int id)
    {
        using var connection = _dbFactory.GetConnection();
        var sql = """
            SELECT * FROM Book 
            WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Book>(sql, new { id });
    }

    public async Task<int> Create(Book book)
    {
        using var connection = _dbFactory.GetConnection();
        var sql = """
            INSERT INTO Book (Title, Author)
            VALUES (@Title, @Author);
            SELECT CAST(last_insert_rowid() AS INT)
        """;
        return await connection.QuerySingleAsync<int>(sql, book);
    }

    public async Task Update(Book book)
    {
        using var connection = _dbFactory.GetConnection();
        var sql = """
            UPDATE Book 
            SET Title = @Title,
                Author = @Author,
                IsBorrowed = @IsBorrowed
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, book);
    }

    public async Task Delete(int id)
    {
        using var connection = _dbFactory.GetConnection();
        var sql = """
            DELETE FROM Book 
            WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}