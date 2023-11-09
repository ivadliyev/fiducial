
using AutoMapper;
using fiducial.bll.Models;
using fiducial.bll.Services.Book;

namespace fiducial.bll.Services.BorrowBook;

public class BorrowBookService : IBorrowBookService
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BorrowBookService(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task Borrow(int id)
    {
        await UpdateIsBorrowed(id, true);
    }

    public async Task Unborrow(int id)
    {
        await UpdateIsBorrowed(id, false);
    }

    /// <summary>
    /// Updates book's property isBorrowed
    /// </summary>
    /// <param name="id">id of the book</param>
    /// <param name="isBorrowed">value of isBorrowed property</param>
    /// <returns>Task</returns>
    /// <exception cref="Exception">If book is not found</exception>
    private async Task UpdateIsBorrowed(int id, bool isBorrowed)
    {
        var book = await _bookService.GetById(id) ?? throw new Exception("Book is not found");
        book.IsBorrowed = isBorrowed;

        await _bookService.Update(book.Id, _mapper.Map<BookDto, BookUpdateDto>(book));
    }
}