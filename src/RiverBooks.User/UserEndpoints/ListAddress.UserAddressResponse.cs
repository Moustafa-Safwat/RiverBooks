namespace RiverBooks.User.UserEndpoints;

internal record UserAddressResponse(
  IList<UserAddressDto> Address
  );
