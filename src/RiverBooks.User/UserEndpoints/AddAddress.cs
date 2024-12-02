using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.User.UseCases.User;

namespace RiverBooks.User.UserEndpoints;
internal class AddAddress(
  ISender sender
  )
  : Endpoint<AddAddressRequest>
{
  public override void Configure()
  {
    Post("/user/address");
    Roles("User");
    Claims(ClaimTypes.Email, ClaimTypes.NameIdentifier);
  }
  public override async Task HandleAsync(AddAddressRequest addAddressRequest, CancellationToken ct)
  {
    var userEmail = User.FindFirstValue(ClaimTypes.Email);
    var command = new AddAddressToUserCommand(
      userEmail!,
      addAddressRequest.AddressLine1,
      addAddressRequest.AddressLine2,
      addAddressRequest.City,
      addAddressRequest.State,
      addAddressRequest.PostalCode,
      addAddressRequest.Country);
    var result = await sender.Send(command);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      await SendOkAsync();
    }
  }
}
