namespace fiducial.tests.Fixtures;

using fiducial.dal.Models;

/// <summary>
/// Static class which stores the list of Books for test
/// </summary>
public static class BooksFixture
{
    public static List<Book> GetBooks()
    {
        return new List<Book>()
        {
            new() { Id = 1, Author = "Wonderfull author", Title = "Wonderfull book", IsBorrowed = false },
            new() { Id = 2, Author = "Inspiring author", Title = "Inspiring book", IsBorrowed = true }
        };
    }

    public static Book GetOneBook()
    {
        return new Book { Author = "Brand new author", Title = "Brand new book", IsBorrowed = false };
    }
}