using System.Net;
using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.User.UseCases.User;

namespace RiverBooks.User.UserEndpoints;
internal class ListAddress (
  ISender sender
  )
  : EndpointWithoutRequest<UserAddressResponse>
{
  public override void Configure()
  {
    Get("user/address");
    Claims(ClaimTypes.Email);
    Roles("User");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var userEmail = User.FindFirstValue(ClaimTypes.Email);

    var query = new GetUserAddressQuery(userEmail!);
    var result = await sender.Send(query);
    if (result.Status== ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      await SendOkAsync(new UserAddressResponse(result.Value));
    }
  }
}
