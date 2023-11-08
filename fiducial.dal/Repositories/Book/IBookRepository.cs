namespace fiducial.dal.Repositories.Book;

using fiducial.dal.Models;

public interface IBookRepository
{
    /// <summary>
    /// Get all book records from database
    /// </summary>
    /// <param name="query">Search query</param>
    /// <returns>IEnumerable<Book></returns>
    Task<IEnumerable<Book>> List(string query);
    /// <summary>
    /// Get one book by its id
    /// </summary>
    /// <param name="id">The id of the record</param>
    /// <returns>Book</returns>
    Task<Book> GetById(int id);
    /// <summary>
    /// Creates a new record in Book table
    /// </summary>
    /// <param name="book">Book model</param>
    /// <returns>integer an id of created record</returns>
    Task<int> Create(Book book);
    /// <summary>
    /// Update book record
    /// </summary>
    /// <param name="book">Book model</param>
    /// <returns>Task</returns>
    Task Update(Book book);
    /// <summary>
    /// Delete book record with given id
    /// </summary>
    /// <param name="id">The id of the record</param>
    /// <returns>Task</returns>
    Task Delete(int id);
}