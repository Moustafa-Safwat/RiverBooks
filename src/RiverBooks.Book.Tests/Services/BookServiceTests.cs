using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RiverBooks.Book.Services;
using RiverBooks.Repository;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace RiverBooks.Tests.Services;
using RiverBooks.Models;

[TestClass]
public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepositoryMock.Object);
    }

    [TestMethod]
    public void GetBookByIdAsync_BookExists_ReturnsBookDtoAsync()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var book = new Book(bookId, "Test Title", "Test Author", 9.99m, new byte[0]);
        _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        // Act
        var result =  _bookService.GetBookByIdAsync(bookId, CancellationToken.None).Result;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(bookId, result.Id);
        Assert.AreEqual("Test Title", result.Title);
        Assert.AreEqual("Test Author", result.Author);
        Assert.AreEqual(9.99m, result.Price);
    }

    [TestMethod]
    public void GetBookByIdAsync_BookDoesNotExist_ReturnsNullAsync()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        // Act
        var result =  _bookService.GetBookByIdAsync(bookId, CancellationToken.None).Result;

        // Assert
        Assert.IsNull(result);
    }
}
