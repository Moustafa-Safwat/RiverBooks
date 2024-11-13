namespace RiverBooks.Book.BookEndpoints;

internal record CreateBookRequest(
  string Author,
  string Title,
  decimal Price
  );
