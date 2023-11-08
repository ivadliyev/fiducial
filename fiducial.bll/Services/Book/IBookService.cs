using fiducial.bll.Models;

namespace fiducial.bll.Services.Book;

/// <summary>
/// Abstraction over book CRUD operations
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Return list of books
    /// </summary>
    /// <param name="query">Optional search query</param>
    /// <returns>IEnumerable<BookDto></returns>
    Task<IEnumerable<BookDto>> List(string? query);
    /// <summary>
    /// Get one book by id
    /// </summary>
    /// <param name="id">id of the book</param>
    /// <returns>BookDto</returns>
    Task<BookDto> GetById(int id);
    /// <summary>
    /// Create book from model
    /// </summary>
    /// <param name="model">BookAddDto</param>
    /// <returns>integer id of created book</returns>
    Task<int> Create(BookAddDto model);
    /// <summary>
    /// Updates existing book
    /// </summary>
    /// <param name="id">id of the book</param>
    /// <param name="model">BookUpdateDto</param>
    /// <returns>Task</returns>
    Task Update(int id, BookUpdateDto model);
    /// <summary>
    /// Deletes book by id
    /// </summary>
    /// <param name="id">id of the book</param>
    /// <returns>Task</returns>
    Task Delete(int id);
}