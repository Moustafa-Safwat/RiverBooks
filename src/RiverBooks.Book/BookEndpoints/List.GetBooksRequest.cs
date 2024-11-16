#pragma warning disable IDE0130
using Microsoft.IdentityModel.Tokens;

namespace RiverBooks.Book;

internal record GetBooksRequest(
  int PageNumber,
  int PageSize
  );
