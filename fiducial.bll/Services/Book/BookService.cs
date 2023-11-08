using AutoMapper;
using fiducial.bll.Models;
using fiducial.dal.Models;
using fiducial.dal.Repositories.Book;

namespace fiducial.bll.Services.Book;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> List(string? query)
    {
        return (await _bookRepository.List(query))
            .Select(_ => _mapper.Map<dal.Models.Book, BookDto>(_))
            .ToList();
    }

    public async Task<int> Create(BookAddDto model)
    {
        return await _bookRepository
            .Create(_mapper.Map<BookAddDto, dal.Models.Book>(model));
    }

    public async Task Delete(int id)
    {
        await _bookRepository.Delete(id);
    }

    public async Task<BookDto> GetById(int id)
    {
        return _mapper
            .Map<dal.Models.Book, BookDto>(await _bookRepository.GetById(id));
    }

    public async Task Update(int id, BookUpdateDto model)
    {
        var book = await _bookRepository.GetById(id) ?? throw new Exception("Book is not found");
        var toUpdate = _mapper.Map(model, book);
        await _bookRepository.Update(toUpdate);
    }
}