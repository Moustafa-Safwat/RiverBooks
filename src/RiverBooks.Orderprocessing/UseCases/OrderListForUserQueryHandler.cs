using Ardalis.Result;
using MediatR;
using RiverBooks.Orderprocessing.Endpoints;
using RiverBooks.Orderprocessing.Repository;

namespace RiverBooks.Orderprocessing.UseCases;
internal class OrderListForUserQueryHandler(
  IOrderRepository orderRepository
  )
  : IRequestHandler<OrderListForUserQuery, Result<IQueryable<OrderListDto>>>
{
  public async Task<Result<IQueryable<OrderListDto>>> Handle(OrderListForUserQuery request,
    CancellationToken cancellationToken)
  {
    var result = await orderRepository.GetOrdersByUserIdAsync(request.UserId,
      request.PageNumber,
      request.PageSize,
      cancellationToken);

    if (result is null)
    {
      return Result.NotFound();
    }

    return Result.Success(result.Select(order => new OrderListDto(
      DateTimeOffset.UtcNow,
      DateTimeOffset.UtcNow,
      order.OrderItems.Sum(item => item.Quantity * item.UnitPrice),
      order.UserId,
      order.Id)).AsQueryable()
      );

  }
}
