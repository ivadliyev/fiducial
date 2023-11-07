namespace fiducial.dal.Repositories.Book;

using fiducial.dal.Models;

public interface IBookRepository
{
    Task<IEnumerable<Book>> List(int id);
    Task<Book> GetById(int id);
    Task Create(Book book);
    Task Update(Book book);
    Task Delete(int id);
}