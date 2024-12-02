using Ardalis.Result;
using MediatR;
using RiverBooks.User.Repository;
using RiverBooks.User.UserEndpoints;

namespace RiverBooks.User.UseCases.User;
internal class GetUserAddressHandler(
  IApplicationUserRepository applicationUserRepository
  )
  : IRequestHandler<GetUserAddressQuery, Result<List<UserAddressDto>>>
{
  public async Task<Result<List<UserAddressDto>>> Handle(GetUserAddressQuery request, CancellationToken cancellationToken)
  {
    var user = await applicationUserRepository.GetApplicationUserWithAddressByEmailAsync(request.userEmail, cancellationToken);
    if (user is null)
    {
      return Result.Unauthorized();
    }
    return Result.Success(user.UserStreetAddresses.Select(address => new UserAddressDto(
      address.Id,
      address.StreetAddress.Street1,
      address.StreetAddress.Street2,
      address.StreetAddress.City,
      address.StreetAddress.State,
      address.StreetAddress.PostalCode,
      address.StreetAddress.Country
    )).ToList());
  }
}
