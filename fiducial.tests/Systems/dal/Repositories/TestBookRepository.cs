using fiducial.dal.Factories;
using fiducial.dal.Repositories.Book;
using fiducial.tests.Fixtures;
using fiducial.tests.Mocks;
using FluentAssertions;
using Moq;

namespace fiducial.tests.Systems.dal.Repositories;

public class TestBookRepository
{
    private readonly IDatabaseConnectionFactory _connectionFactory;
    public TestBookRepository()
    {
        var db = new DatabaseMock();
        db.Insert(BooksFixture.GetBooks());

        var connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();
        connectionFactoryMock.Setup(c => c.GetConnection()).Returns(db.OpenConnection());

        _connectionFactory = connectionFactoryMock.Object;
    }

    [Fact]
    public async Task ListBooks_WhenCalled_IsEquivalent()
    {
        // Act
        var result = await new BookRepository(_connectionFactory).List(null);

        // Assert
        result.Should().BeEquivalentTo(BooksFixture.GetBooks());
    }

    [Fact]
    public async Task ListBooks_WhenCalledWithFilter_HasOnly1Item()
    {
        // Arrange
        var bookTitle = BooksFixture.GetBooks()[0].Title;

        // Act
        var result = await new BookRepository(_connectionFactory).List(bookTitle);

        // Assert
        result.Should().HaveCount(1).And.ContainEquivalentOf(BooksFixture.GetBooks()[0]);
    }

    [Fact]
    public async Task GetBookById_WhenCalled_ReturnsBook()
    {
        // Arrange
        var book = BooksFixture.GetBooks()[0];

        // Act
        var result = await new BookRepository(_connectionFactory).GetById(book.Id);

        // Assert
        result.Should().BeEquivalentTo(book);
    }

    [Fact]
    public async Task CreateBook_WhenCalled_CreatesBook()
    {
        // Arrange
        var bookToAdd = BooksFixture.GetOneBook();
        var bookRepository = new BookRepository(_connectionFactory);

        // Act
        var id = await bookRepository.Create(bookToAdd);
        var result = await bookRepository.GetById(id);

        // Assert
        id.Should().BeGreaterThan(0);
        result.Should().NotBe(null);
    }

    [Fact]
    public async Task UpdateBook_WhenCalled_UpdatesBook()
    {
        // Arrange
        var bookToUpdate = BooksFixture.GetBooks()[0];
        var bookRepository = new BookRepository(_connectionFactory);
        var newTitle = "Updated title";

        // Act
        bookToUpdate.Title = newTitle;
        await bookRepository.Update(bookToUpdate);
        var result = await bookRepository.GetById(bookToUpdate.Id);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().BeEquivalentTo(newTitle);
    }

    [Fact]
    public async Task DeleteBook_WhenCalled_DeletesBook()
    {
        // Arrange
        var bookToDelete = BooksFixture.GetBooks()[0];
        var bookRepository = new BookRepository(_connectionFactory);

        // Act
        await bookRepository.Delete(bookToDelete.Id);
        var result = await bookRepository.List(null);

        // Assert
        result.Should().NotContainEquivalentOf(bookToDelete);
    }
}