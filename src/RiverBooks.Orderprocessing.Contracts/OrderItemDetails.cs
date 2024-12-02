using System;

namespace RiverBooks.Orderprocessing.Contracts;

public record OrderItemDetails(
  Guid BookId,
  int Quantity,
  decimal UnitPrice,
  string Description
  );
