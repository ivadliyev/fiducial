namespace fiducial.dal.Repositories.Book;

using System.Collections.Generic;
using System.Threading.Tasks;
using fiducial.dal.Models;

public class BookRepository : IBookRepository
{
    public Task Create(Book book)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Book>> List(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Book book)
    {
        throw new NotImplementedException();
    }
}