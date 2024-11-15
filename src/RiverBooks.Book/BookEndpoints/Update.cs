using FastEndpoints;
using RiverBooks.Book.Dtos;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;
/// <summary>
/// Endpoint for updating a book.
/// </summary>
internal class Update(IBookService bookService)
  : Endpoint<UpdateBookRequest, BookDto>
{

  /// <summary>
  /// Configures the endpoint.
  /// </summary>
  public override void Configure()
  {
    Put("/book");
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the update book request.
  /// </summary>
  /// <param name="req">The update book request.</param>
  /// <param name="ct">The cancellation token.</param>
  /// <returns>A task that represents the asynchronous operation.</returns>
  public async override Task HandleAsync(UpdateBookRequest req, CancellationToken ct)
  {
    var bookDto = new BookDto(req.Id, req.Title, req.Author, req.Price);
    await bookService.UpdateBookAsync(bookDto, ct);
    await SendCreatedAtAsync<GetById>(new { req.Id }, bookDto);
  }
}
