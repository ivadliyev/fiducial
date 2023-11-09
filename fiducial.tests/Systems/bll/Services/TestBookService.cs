using AutoMapper;
using fiducial.dal.Repositories.Book;
using fiducial.tests.Fixtures;
using fiducial.bll.Helpers;
using Moq;
using fiducial.bll.Services.Book;
using FluentAssertions;
using fiducial.bll.Models;
using fiducial.dal.Models;

namespace fiducial.tests.Systems.bll.Services;

public class TestBookService
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly IMapper _mapper;

    public TestBookService()
    {
        var mockMapper = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new AutomapperProfileBll());
        });
        _mapper = mockMapper.CreateMapper();

        _mockBookRepository = new Mock<IBookRepository>();
        _mockBookRepository
            .Setup(_ => _.List(null))
            .ReturnsAsync(BooksFixture.GetBooks);
        _mockBookRepository
            .Setup(_ => _.GetById(BooksFixture.GetBooks()[0].Id))
            .ReturnsAsync(BooksFixture.GetBooks()[0]);
        _mockBookRepository
            .Setup(_ => _.Create(It.IsAny<Book>()))
            .ReturnsAsync(5);
        _mockBookRepository
            .Setup(_ => _.Update(It.IsAny<Book>()))
            .Verifiable();
        _mockBookRepository
            .Setup(_ => _.Delete(It.IsAny<int>()))
            .Verifiable();
    }

    [Fact]
    public async Task List_WhenCalled_InvokesRepositoryAndReturnsBookDto()
    {
        //Arrange
        var sut = new BookService(_mockBookRepository.Object, _mapper);
        //Act
        var result = await sut.List(null);
        //Assert
        _mockBookRepository.Verify(_ => _.List(null), Times.Once);
        result.Should().AllBeOfType(typeof(BookDto));
        result.Should().HaveCount(BooksFixture.GetBooks().Count);
    }

    [Fact]
    public async Task GetById_WhenCalled_InvokesRepositoryAndReturnsBookDto()
    {
        //Arrange
        var sut = new BookService(_mockBookRepository.Object, _mapper);
        var bookToGet = BooksFixture.GetBooks()[0];
        //Act
        var result = await sut.GetById(bookToGet.Id);
        //Assert
        _mockBookRepository.Verify(_ => _.GetById(bookToGet.Id), Times.Once);
        result.Should().BeOfType(typeof(BookDto));
        result.Should().BeEquivalentTo(bookToGet, options => options
            .Including(x => x.Id)
            .Including(x => x.Title)
            .Including(x => x.Author));
    }

    [Fact]
    public async Task Create_WhenCalled_InvokesRepositoryAndReturnsInt()
    {
        //Arrange
        var sut = new BookService(_mockBookRepository.Object, _mapper);
        //Act
        var result = await sut.Create(new BookAddDto());
        //Assert
        _mockBookRepository.Verify(_ => _.Create(It.IsAny<Book>()), Times.Once);
        result.Should().BeOfType(typeof(int));
        result.Should().Be(5);
    }

    [Fact]
    public async Task Update_WhenCalled_InvokesRepository()
    {
        //Arrange
        var sut = new BookService(_mockBookRepository.Object, _mapper);
        //Act
        await sut.Update(1, new BookUpdateDto());
        //Assert
        _mockBookRepository.Verify(_ => _.Update(It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenCalled_InvokesRepository()
    {
        //Arrange
        var sut = new BookService(_mockBookRepository.Object, _mapper);
        //Act
        await sut.Delete(1);
        //Assert
        _mockBookRepository.Verify(_ => _.Delete(It.IsAny<int>()), Times.Once);
    }
}