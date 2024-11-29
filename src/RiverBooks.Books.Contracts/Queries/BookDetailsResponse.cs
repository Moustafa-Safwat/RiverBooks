namespace RiverBooks.Books.Contracts.Queries;
public record BookDetailsResponse(
  Guid BookId,
  string Author,
  string Title,
  decimal UnitPrice
  );
