#pragma warning disable IDE0130
namespace RiverBooks.Book;

internal record GetBooksRequest(
  int PageNumber,
  int PageSize
  );
