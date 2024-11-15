using RiverBooks.Repository;

namespace RiverBooks.Book.Repository;

using RiverBooks.Models;
/// <summary>
/// Repository class for managing books.
/// </summary>
internal class BookRepository : IBookRepository
{
    /// <summary>
    /// A collection of books.
    /// </summary>
    private static readonly IList<Book> _books;

    /// <summary>
    /// Static constructor to initialize the book repository.
    /// </summary>
    static BookRepository()
    {
        _books = SeedBooksData().ToList();
    }

    /// <summary>
    /// Seeds the initial data for the book repository.
    /// </summary>
    /// <returns>An enumerable collection of books.</returns>
    private static IEnumerable<Book> SeedBooksData()
    {
        yield return new(Guid.Parse("11111111-1111-1111-1111-111111111111"), "The Great Gatsby", "F. Scott Fitzgerald", 10.99m);
        yield return new(Guid.Parse("22222222-2222-2222-2222-222222222222"), "1984", "George Orwell", 8.99m);
        yield return new(Guid.Parse("33333333-3333-3333-3333-333333333333"), "To Kill a Mockingbird", "Harper Lee", 12.99m);
        yield return new(Guid.Parse("44444444-4444-4444-4444-444444444444"), "Pride and Prejudice", "Jane Austen", 9.99m);
        yield return new(Guid.Parse("55555555-5555-5555-5555-555555555555"), "The Catcher in the Rye", "J.D. Salinger", 11.99m);
    }

  /// <summary>
  /// Checks if the cancellation token has requested cancellation.
  /// </summary>
  /// <param name="cancellation">The cancellation token.</param>
  private static void CheckCancellation(CancellationToken cancellation)
  {
    if (cancellation.IsCancellationRequested)
    {
      cancellation.ThrowIfCancellationRequested();
    }
  }

  /// <summary>
  /// Adds a new book asynchronously.
  /// </summary>
  /// <param name="book">The book to add.</param>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public Task AddBookAsync(Book book, CancellationToken cancellation)
  {
    CheckCancellation(cancellation);
    _books.Add(book);
    return Task.CompletedTask;
  }

  /// <summary>
  /// Deletes a book asynchronously by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the book to delete.</param>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task DeleteBookAsync(Guid id, CancellationToken cancellation)
  {
    var book = await GetBookByIdAsync(id, cancellation);
    if (book is not null)
    {
      _books.Remove(book);
    }
  }

  /// <summary>
  /// Gets all books asynchronously with pagination.
  /// </summary>
  /// <param name="pagNumber">The page number.</param>
  /// <param name="pageSize">The size of the page.</param>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation, containing a queryable collection of books.</returns>
  public Task<IQueryable<Book>> GetAllBooksAsync(int pagNumber, int pageSize, CancellationToken cancellation)
  {
    CheckCancellation(cancellation);
    var books = _books.Skip(pageSize * (pagNumber - 1)).Take(pageSize);
    return Task.FromResult(books.AsQueryable());
  }

  /// <summary>
  /// Gets a book by its identifier asynchronously.
  /// </summary>
  /// <param name="id">The identifier of the book.</param>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation, containing the book if found; otherwise, null.</returns>
  public Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellation)
  {
    CheckCancellation(cancellation);
    var book = _books.FirstOrDefault(book => book.Id == id);
    return Task.FromResult(book);
  }

  /// <summary>
  /// Saves changes asynchronously.
  /// </summary>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public Task SaveChangesAcync(CancellationToken cancellation)
  {
    CheckCancellation(cancellation);
    return Task.CompletedTask;
  }

  /// <summary>
  /// Updates a book asynchronously.
  /// </summary>
  /// <param name="book">The book to update.</param>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task UpdateBookAsync(Book book, CancellationToken cancellation)
  {
    var bookResult = await GetBookByIdAsync(book.Id, cancellation);
    if (bookResult is not null)
    {
      bookResult.UpdateAuthor(book.Author);
      bookResult.UpdateTitle(book.Title);
      bookResult.UpdatePrice(book.Price);
    }
  }

  /// <summary>
  /// Updates the price of a book asynchronously.
  /// </summary>
  /// <param name="id">The identifier of the book.</param>
  /// <param name="newPrice">The new price of the book.</param>
  /// <param name="cancellation">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation, containing the updated book if found; otherwise, null.</returns>
  public async Task<Book?> UpdateBookPrice(Guid id, decimal newPrice, CancellationToken cancellation)
  {
    var book = await GetBookByIdAsync(id, cancellation);
    if (book is not null)
    {
      book.UpdatePrice(newPrice);
    }
    return book;
  }
}
