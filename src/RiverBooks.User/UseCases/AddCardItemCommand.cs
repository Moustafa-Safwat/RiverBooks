using Ardalis.Result;
using MediatR;

namespace RiverBooks.User.UseCases;

internal record AddCardItemCommand(
  Guid BookId, 
  int Quantity, 
  string EmailAddress
  )
  :IRequest<Result>;
