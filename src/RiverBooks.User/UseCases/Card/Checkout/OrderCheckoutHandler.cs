using Ardalis.Result;
using MediatR;
using RiverBooks.Orderprocessing.Contracts;
using RiverBooks.User.Data;
using RiverBooks.User.Repository;

namespace RiverBooks.User.UseCases.Card.Checkout;
internal class OrderCheckoutHandler(
  ISender sender,
  IApplicationUserRepository applicationUserRepository
  )
  : IRequestHandler<OrderCheckoutCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(OrderCheckoutCommand request, CancellationToken cancellationToken)
  {
    // Get user by email address
    var user = await applicationUserRepository.GetApplicationUSerByEmailAsync(request.UserEmail, cancellationToken);

    if (user is null)
    {
      return Result.Unauthorized();
    }
    var items = user.CardItems.Select(item => new OrderItemDetails(item.BookId,
      item.Quantity,
      item.Price,
      item.Description)).ToList();
    // will call the handerl in the order proceasing module to create the order
    var command = new CreateOrderCommand(Guid.Parse(user.Id), request.ShippingAddressId, request.BillingAddressId, items);
    var result = await sender.Send(command);
    if (!result.IsSuccess)
    {
      return result.Map(x => x.OrderId);
    }
    user.ClearCart();
    await applicationUserRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(result.Value.OrderId);
  }
}



