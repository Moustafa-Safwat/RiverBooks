using Ardalis.Result;
using MediatR;
using RiverBooks.Orderprocessing.Contracts;
using RiverBooks.Orderprocessing.Entities;
using RiverBooks.Orderprocessing.Repository;

namespace RiverBooks.Orderprocessing.Integration;
internal class CreateOrderHandler(
  IOrderRepository orderRepository
  )
  : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var items = request.Items
      .Select(item => new OrderItem(item.BookId, item.Quantity, item.UnitPrice, item.Description))
      .ToList();
    // TODO: Remove the dumy adress with the correct address
    Address dumyAddress = new Address("dummy", "dummy", "dummy", "dummy", "dummy", "dummy");
    Order order = Order.Factory.Create(request.UserId, dumyAddress, dumyAddress, items);

    var orderId = await orderRepository.AddOrderAsync(order, cancellationToken);
    await orderRepository.SaveChangesAsync(cancellationToken);

    return Result<OrderDetailsResponse>.Success(new OrderDetailsResponse(orderId));
  }
}
