namespace RiverBooks.User.CardEndpoints;

internal record AddCartItemRequest(
  Guid BookId,
  int Quantity
  );
