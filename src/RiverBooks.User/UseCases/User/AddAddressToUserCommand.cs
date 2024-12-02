using Ardalis.Result;
using MediatR;

namespace RiverBooks.User.UseCases.User;

internal record AddAddressToUserCommand(
  string UserEmail,
  string AddressLine1,
  string AddressLine2,
  string City,
  string State,
  string PostalCode,
  string Country) : IRequest<Result>;
