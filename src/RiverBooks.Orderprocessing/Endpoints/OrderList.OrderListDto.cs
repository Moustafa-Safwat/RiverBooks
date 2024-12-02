namespace RiverBooks.Orderprocessing.Endpoints;

internal record OrderListDto(
  DateTimeOffset CreateDate,
  DateTimeOffset ShippedDate,
  decimal Total,
  Guid UserId,
  Guid OrderId
  );
