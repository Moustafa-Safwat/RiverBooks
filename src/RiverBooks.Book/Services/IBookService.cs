using RiverBooks.Book.Dtos;

namespace RiverBooks.Book.Services;

internal interface IBookService
{
  Task<BookDto?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
  Task<IQueryable<BookDto>> GetBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

  Task AddBookAsync(BookDto bookDto, CancellationToken cancellationToken);
  Task UpdateBookAsync(BookDto bookDto, CancellationToken cancellationToken);
  Task DeleteBookAsync(Guid id, CancellationToken cancellationToken);
  Task<BookDto?> UpdateBookPrice(Guid id, decimal newPrice, CancellationToken cancellationToken);

}
