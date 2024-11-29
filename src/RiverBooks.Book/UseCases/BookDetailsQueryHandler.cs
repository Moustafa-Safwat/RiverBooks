using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts.Queries;
using RiverBooks.Repository;

namespace RiverBooks.Book.UseCases;
/// <summary>
/// Handles the query for retrieving book details.
/// </summary>
/// <param name="bookRepository">The repository to access book data.</param>
internal class BookDetailsQueryHandler(
  IBookRepository bookRepository
  )
  : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
    /// <summary>
    /// Handles the request to get book details by book ID.
    /// </summary>
    /// <param name="request">The query request containing the book ID.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing the book details response or a not found result.</returns>
    public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await bookRepository.GetBookByIdAsync(request.BookId, cancellationToken);
        if (result is null)
        {
            return Result.NotFound();
        }
        return new BookDetailsResponse(
          result.Id,
          result.Author,
          result.Title,
          result.Price);
    }
}
