using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.User.UseCases.Card.Checkout;

namespace RiverBooks.User.CardEndpoints;
internal class Checkout(
  ISender sender
  )
  : Endpoint<OrderCheckoutRequest, OrderCheckoutResponse>
{
  public override void Configure()
  {
    Post("/card/checkout");
    Roles("User");
    Claims(ClaimTypes.Email, ClaimTypes.NameIdentifier);
  }

  public override async Task HandleAsync(OrderCheckoutRequest orderCheckoutRequest, CancellationToken ct)
  {
    var userEmail = User.FindFirstValue(ClaimTypes.Email);

    var command = new OrderCheckoutCommand(
      userEmail!,
      orderCheckoutRequest.ShippingAddressId,
      orderCheckoutRequest.BillingAddressId);

    var result = await sender.Send(command);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      await SendOkAsync(new OrderCheckoutResponse(result.Value));
    }
  }
}
