using Ardalis.Result;
using MediatR;

namespace RiverBooks.User.UseCases.Card.Checkout;

internal record OrderCheckoutCommand(
  string UserEmail,
  Guid ShippingAddressId,
  Guid BillingAddressId
  ) : IRequest<Result<Guid>>;
