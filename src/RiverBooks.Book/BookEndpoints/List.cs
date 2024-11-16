using FastEndpoints;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;

/// <summary>
/// Endpoint to handle the retrieval of all books.
/// </summary>
internal class List(IBookService bookService)
    : Endpoint<GetBooksRequest, GetBooksDto>
{
  /// <summary>
  /// Configures the endpoint settings such as route and access permissions.
  /// </summary>
  public override void Configure()
  {
    Get("/books");
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the request to get all books asynchronously.
  /// </summary>
  /// <param name="ct">Cancellation token to cancel the operation.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public override async Task HandleAsync(GetBooksRequest request, CancellationToken ct)
  {
    var books = await bookService.GetBooksAsync(request.PageNumber, request.PageSize, ct);
    await SendAsync(new(books.ToList()));
  }
}
