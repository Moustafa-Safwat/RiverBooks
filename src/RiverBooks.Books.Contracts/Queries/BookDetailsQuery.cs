using Ardalis.Result;
using MediatR;

namespace RiverBooks.Books.Contracts.Queries;

public record BookDetailsQuery (
  Guid BookId
  )
  : IRequest<Result<BookDetailsResponse>>;
