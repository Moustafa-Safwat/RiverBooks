using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.User.UseCases;

namespace RiverBooks.User.CardEndpoints;
internal class AddCardItem(ISender sender)
  : Endpoint<AddCartItemRequest>
{
  public override void Configure()
  {
    Post("/card");
    Claims(ClaimTypes.Email);
    Roles("User", "Admin");
  }

  public async override Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
  {
    var userEmail = User.FindFirstValue(ClaimTypes.Email);
    var command = new AddCardItemCommand(req.BookId, req.Quantity, userEmail!);
    var result = await sender.Send(command, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync();
      return;
    }
    await SendOkAsync();
  }
}
