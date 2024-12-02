using Microsoft.EntityFrameworkCore;
using RiverBooks.Orderprocessing.Data;
using RiverBooks.Orderprocessing.Entities;

namespace RiverBooks.Orderprocessing.Repository;

internal class EFOrderRepository(
  OrderProcessingDbContext context
  )
  : IOrderRepository
{
  public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await context.Set<Order>().FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
  }

  public async Task<Guid> AddOrderAsync(Order order, CancellationToken cancellationToken)
  {
    var result = await context.Set<Order>().AddAsync(order, cancellationToken);
    return result.Entity.Id;
  }

  public async Task SaveChangesAsync(CancellationToken cancellationToken)
  {
    await context.SaveChangesAsync(cancellationToken);
  }

  public Task<IQueryable<Order>> GetOrdersByUserIdAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
  {
    return Task.FromResult(
      context.Set<Order>().Where(order => order.UserId == userId)
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .AsQueryable()
      );
  }
}
