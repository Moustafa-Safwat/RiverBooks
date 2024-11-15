using FastEndpoints;
using RiverBooks.Book.Dtos;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;
/// <summary>
/// Endpoint to handle the retrieval of a book by its ID.
/// </summary>
internal class GetById(IBookService bookService)
  : Endpoint<GetByIdRequest, BookDto>
{
    /// <summary>
    /// Configures the endpoint settings.
    /// </summary>
    public override void Configure()
    {
        Get("/book/{id}");
        AllowAnonymous();
    }

    /// <summary>
    /// Handles the request to get a book by its ID.
    /// </summary>
    /// <param name="req">The request containing the book ID.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var result = await bookService.GetBookByIdAsync(req.Id, ct);
        if (result is null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(result);
    }
}
