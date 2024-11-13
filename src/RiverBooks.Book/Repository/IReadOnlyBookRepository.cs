
#pragma warning disable IDE0130
namespace RiverBooks.Repository;
#pragma warning restore IDE0130

using RiverBooks.Models;

internal interface IReadOnlyBookRepository
{
  Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellation);
  Task<IQueryable<Book>> GetAllBooksAsync(int pagNumber, int pageSize, CancellationToken cancellation);
}
