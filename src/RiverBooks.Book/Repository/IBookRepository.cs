
#pragma warning disable IDE0130
namespace RiverBooks.Repository;
#pragma warning restore IDE0130

using RiverBooks.Models;

internal interface IBookRepository : IReadOnlyBookRepository
{
  Task AddBookAsync(Book book, CancellationToken cancellation);
  Task UpdateBookAsync(Book book, CancellationToken cancellation);
  Task DeleteBookAsync(Guid id, CancellationToken cancellation);
  Task<Book?> UpdateBookPrice(Guid id, decimal newPrice, CancellationToken cancellation);
  Task SaveChangesAcync(CancellationToken cancellation);
}
