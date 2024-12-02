using Ardalis.Result;
using MediatR;

namespace RiverBooks.User.UseCases.Card.AddItem;

internal record AddCardItemCommand(
  Guid BookId,
  int Quantity,
  string EmailAddress
  )
  : IRequest<Result>;
