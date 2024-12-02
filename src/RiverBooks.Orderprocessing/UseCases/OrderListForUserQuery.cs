using Ardalis.Result;
using MediatR;
using RiverBooks.Orderprocessing.Endpoints;

namespace RiverBooks.Orderprocessing.UseCases;
internal record OrderListForUserQuery(
  Guid UserId,
  int PageNumber,
  int PageSize
  )
  : IRequest<Result<IQueryable<OrderListDto>>>;
