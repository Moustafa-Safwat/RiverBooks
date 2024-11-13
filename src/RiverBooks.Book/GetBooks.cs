using FastEndpoints;

namespace RiverBooks.Book;

/// <summary>
/// Endpoint to handle the retrieval of all books.
/// </summary>
internal class GetBooks(IBookService bookService)
    : EndpointWithoutRequest<GetBooksDto>
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
    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(bookService.GetAllBooks());
    }
}

