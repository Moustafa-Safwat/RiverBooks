using Ardalis.Result;
using MediatR;
using RiverBooks.User.UserEndpoints;

namespace RiverBooks.User.UseCases.User;

internal record GetUserAddressQuery(string userEmail)
  : IRequest<Result<List<UserAddressDto>>>;
