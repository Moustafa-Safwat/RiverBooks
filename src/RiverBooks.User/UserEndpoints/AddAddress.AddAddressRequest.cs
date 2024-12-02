namespace RiverBooks.User.UserEndpoints;

internal record AddAddressRequest(
  string AddressLine1,
  string AddressLine2,
  string City,
  string State,
  string PostalCode,
  string Country
  );
