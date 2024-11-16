using FastEndpoints;
using RiverBooks.Book.Dtos;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;
/// <summary>
/// Endpoint for creating a new book.
/// </summary>
internal class Create(IBookService bookService)
  : Endpoint<CreateBookRequest, BookDto>
{
  /// <summary>
  /// Configures the endpoint settings.
  /// </summary>
  public override void Configure()
  {
    Post("/book");
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the creation of a new book.
  /// </summary>
  /// <param name="req">The request containing book details.</param>
  /// <param name="ct">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
  {
    var bookToAdd = new BookDto(Guid.NewGuid(), req.Title, req.Author, req.Price);
    await bookService.AddBookAsync(bookToAdd, ct);
    var addedBook = await bookService.GetBookByIdAsync(bookToAdd.Id,ct);
    await SendCreatedAtAsync<GetById>(new { bookToAdd.Id }, addedBook!);
  }
}
