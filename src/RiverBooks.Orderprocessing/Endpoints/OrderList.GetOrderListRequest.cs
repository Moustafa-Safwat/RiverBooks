namespace RiverBooks.Orderprocessing.Endpoints;

internal record GetOrderListRequest(
  int PageNumber,
  int PageSize);
