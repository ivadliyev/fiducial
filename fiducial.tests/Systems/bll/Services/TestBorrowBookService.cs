using AutoMapper;
using fiducial.bll.Helpers;
using fiducial.bll.Models;
using fiducial.bll.Services.Book;
using fiducial.bll.Services.BorrowBook;
using Moq;

namespace fiducial.tests.Systems.bll.Services;

public class TestBorrowBookService
{
    private readonly Mock<IBookService> _mockBookService;
    private readonly IMapper _mapper;

    public TestBorrowBookService()
    {
        var mockMapper = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new AutomapperProfileBll());
        });
        _mapper = mockMapper.CreateMapper();

        _mockBookService = new Mock<IBookService>();
        _mockBookService
            .Setup(_ => _.GetById(It.IsAny<int>()))
            .ReturnsAsync(new BookDto());
        _mockBookService
            .Setup(_ => _.Update(It.IsAny<int>(), It.IsAny<BookUpdateDto>()))
            .Verifiable();
    }

    [Fact]
    public async Task Borrow_WhenCalled_InvokesRepository()
    {
        //Arrange
        var sut = new BorrowBookService(_mockBookService.Object, _mapper);
        //Act
        await sut.Borrow(1);
        //Assert
        _mockBookService.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<BookUpdateDto>()), Times.Once);
    }

    [Fact]
    public async Task Unborrow_WhenCalled_InvokesRepository()
    {
        //Arrange
        var sut = new BorrowBookService(_mockBookService.Object, _mapper);
        //Act
        await sut.Unborrow(1);
        //Assert
        _mockBookService.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<BookUpdateDto>()), Times.Once);
    }
}