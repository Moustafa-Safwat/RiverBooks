using System.Threading.Tasks.Sources;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Book.Data;
using RiverBooks.Models;
using RiverBooks.Repository;

namespace RiverBooks.Book.Repository;
internal class EFBookRepository(BookDbContext context)
  : IBookRepository
{
  public async Task AddBookAsync(RiverBooks.Models.Book book, CancellationToken cancellation)
  {
    await context.Books.AddAsync(book, cancellation);
  }

  public async Task DeleteBookAsync(Guid id, CancellationToken cancellation)
  {
    var book = await GetBookByIdAsync(id, cancellation);
    if (book is not null)
    {
      context.Books.Remove(book);
    }
  }

  public Task<IQueryable<RiverBooks.Models.Book>> GetAllBooksAsync(int pagNumber, int pageSize, CancellationToken cancellation)
  {
    return Task.FromResult(context.Books.Skip((pagNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .AsQueryable());
  }

  public async Task<RiverBooks.Models.Book?> GetBookByIdAsync(Guid id, CancellationToken cancellation)
  {
    var book = await context.Books.FindAsync([id], cancellation);
    if (book is null)
    {
      return null;
    }
    return book;
  }

  public async Task SaveChangesAcync(CancellationToken cancellation)
  {
    await context.SaveChangesAsync();
  }

  public async Task UpdateBookAsync(RiverBooks.Models.Book book, CancellationToken cancellation)
  {
    var bookFromDb = await GetBookByIdAsync(book.Id, cancellation);
    if (bookFromDb is null)
    {
      return;
    }
    try
    {
      bookFromDb.UpdatePrice(book.Price);
      context.Update(bookFromDb);
    }
    catch (DbUpdateConcurrencyException)
    {
      // TODO: handel the case of Concurrency Exception
      return;
    }
  }

    public async Task<RiverBooks.Models.Book?> UpdateBookPrice(Guid id, decimal newPrice, CancellationToken cancellation)
    {
        var bookFromDb = await GetBookByIdAsync(id, cancellation);
        if (bookFromDb is null)
        {
            // Handle the case of book not found
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }
        context.Books.Entry(bookFromDb).Property(book => book.Price).CurrentValue = newPrice;
        return bookFromDb;
    }
}
