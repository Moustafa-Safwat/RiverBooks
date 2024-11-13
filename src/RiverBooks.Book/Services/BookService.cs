using RiverBooks.Book.Dtos;
using RiverBooks.Repository;

namespace RiverBooks.Book.Services;

using RiverBooks.Models;
internal class BookService(IBookRepository bookRepository) : IBookService
{
  public async Task AddBookAsync(BookDto bookDto, CancellationToken cancellationToken)
  {
    Book book = new Book(bookDto.Id, bookDto.Title, bookDto.Author, bookDto.Price);
    await bookRepository.AddBookAsync(book, cancellationToken);
    await bookRepository.SaveChangesAcync(cancellationToken);
  }

  public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken)
  {
    await bookRepository.DeleteBookAsync(id, cancellationToken);
    await bookRepository.SaveChangesAcync(cancellationToken);
  }

  public async Task<BookDto?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    var book = await bookRepository.GetBookByIdAsync(id, cancellationToken);
    if (book is not null)
    {
      return new BookDto(book.Id, book.Title, book.Author, book.Price);
    }
    return null;
  }

  public Task<IQueryable<BookDto>> GetBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
  {
    var result = bookRepository.GetAllBooksAsync(pageNumber, pageSize, cancellationToken)
      .Result
      .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price))
      .ToList();
    return Task.FromResult(result.AsQueryable());
  }

  public async Task UpdateBookAsync(BookDto bookDto, CancellationToken cancellationToken)
  {
    await bookRepository.UpdateBookAsync(new Book(bookDto.Id, bookDto.Title, bookDto.Author, bookDto.Price), cancellationToken);
    await bookRepository.SaveChangesAcync(cancellationToken);
  }

  public async Task<BookDto?> UpdateBookPrice(Guid id, decimal newPrice, CancellationToken cancellationToken)
  {
    var result = await bookRepository.UpdateBookPrice(id, newPrice, cancellationToken);
    await bookRepository.SaveChangesAcync(cancellationToken);
    if (result is not null)
    {
      return new BookDto(result.Id, result.Title, result.Author, result.Price);
    }
    return null;
  }
}
