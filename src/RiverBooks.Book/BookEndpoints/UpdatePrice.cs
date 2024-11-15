using FastEndpoints;
using RiverBooks.Book.Dtos;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;
/// <summary>
/// Endpoint for updating the price of a book.
/// </summary>
/// <param name="bookService">Service to handle book operations.</param>
internal class UpdatePrice(IBookService bookService)
  : Endpoint<UpdateBookPriceRequest, BookDto>
{
  /// <summary>
  /// Configures the endpoint settings.
  /// </summary>
  public override void Configure()
  {
    Patch("/book/{id}/pricehistory");
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the request to update the book price.
  /// </summary>
  /// <param name="req">The request containing the book ID and new price.</param>
  /// <param name="ct">Cancellation token.</param>
  public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
  {
    await bookService.UpdateBookPrice(req.Id, req.NewPrice, ct);
    var updatedBook = await bookService.GetBookByIdAsync(req.Id, ct);
    if (updatedBook is null)
    {
      await SendErrorsAsync();
      return;
    }
    await SendAsync(updatedBook);
  }
}
