namespace fiducial.bll.Services.BorrowBook;

/// <summary>
/// Interface to mark book as borrowed and unborrowed
/// </summary>
public interface IBorrowBookService
{
    /// <summary>
    /// Mark book as borrowed
    /// </summary>
    /// <param name="id">id of the book</param>
    /// <returns>Task</returns>
    Task Borrow(int id);
    /// <summary>
    /// Mark book as unborrowed
    /// </summary>
    /// <param name="id">id of the book</param>
    /// <returns>Task</returns>
    Task Unborrow(int id);
}