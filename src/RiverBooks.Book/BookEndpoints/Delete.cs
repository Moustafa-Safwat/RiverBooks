using FastEndpoints;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;
/// <summary>
/// Endpoint for deleting a book.
/// </summary>
/// <param name="bookService">Service for book operations.</param>
internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
{
    /// <summary>
    /// Configures the endpoint.
    /// </summary>
    public override void Configure()
    {
        Delete("/book/{id}");
        AllowAnonymous();
    }

    /// <summary>
    /// Handles the delete book request.
    /// </summary>
    /// <param name="req">The delete book request.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async override Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        await bookService.DeleteBookAsync(req.Id, ct);
        await SendNoContentAsync();
    }
}
