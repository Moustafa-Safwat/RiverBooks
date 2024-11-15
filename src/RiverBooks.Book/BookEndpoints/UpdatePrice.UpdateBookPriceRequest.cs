namespace RiverBooks.Book.BookEndpoints;

internal record UpdateBookPriceRequest(
  Guid Id,
  decimal NewPrice
  );
