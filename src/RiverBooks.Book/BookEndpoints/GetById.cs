using FastEndpoints;
using RiverBooks.Book.Dtos;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.BookEndpoints;
internal class GetById(IBookService bookService)
  : Endpoint<GetByIdRequest, BookDto>
{
  public override void Configure()
  {
    Get("/book/{id}");
    AllowAnonymous();
  }

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
