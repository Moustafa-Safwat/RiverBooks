namespace RiverBooks.Book.BookEndpoints;

internal record UpdateBookRequest(
  Guid Id,
  string Title,
  string Author,
  decimal Price
  );
