using Ardalis.Result;
using MediatR;
using RiverBooks.User.Data;
using RiverBooks.User.Repository;

namespace RiverBooks.User.UseCases.User;
internal class AddAddressToUserHandler(
  IApplicationUserRepository applicationUserRepository
  ) : IRequestHandler<AddAddressToUserCommand, Result>
{
  public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
  {
    // get user by email
    var user = await applicationUserRepository.GetApplicationUSerByEmailAsync(request.UserEmail, cancellationToken);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    var addressToAdd = new Address(
      request.AddressLine1,
      request.AddressLine2,
      request.City,
      request.State,
      request.PostalCode,
      request.Country
      );

    user.AddAddress(addressToAdd);
    await applicationUserRepository.SaveChangesAsync(cancellationToken);
    return Result.Success();
  }
}
