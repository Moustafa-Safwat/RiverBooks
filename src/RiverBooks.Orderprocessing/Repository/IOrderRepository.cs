using RiverBooks.Orderprocessing.Entities;

namespace RiverBooks.Orderprocessing.Repository;

internal interface IOrderRepository
{
  Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
  Task<IQueryable<Order>> GetOrdersByUserIdAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
  Task SaveChangesAsync(CancellationToken cancellationToken);
  Task<Guid> AddOrderAsync(Order order, CancellationToken cancellationToken);

}
