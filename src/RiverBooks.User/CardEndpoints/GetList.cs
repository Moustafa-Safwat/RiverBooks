using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.User.UseCases;
using RiverBooks.User.UseCases.Card.ListItems;

namespace RiverBooks.User.CardEndpoints;
internal class GetList(ISender sender) : EndpointWithoutRequest<CardResponse>
{
  public override void Configure()
  {
    Get("/card");
    Claims(ClaimTypes.Email);
    Roles("User", "Admin");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    // get the email from the claims from the token
    var emailAddress = User.FindFirst(ClaimTypes.Email)!.Value;
    // Get the user by email address
    var query = new ListItemCardQuery(emailAddress);
    var result = await sender.Send(query, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync();
      return;
    }
    var response = new CardResponse(result.Value);
    await SendOkAsync(response, ct);
  }
}
