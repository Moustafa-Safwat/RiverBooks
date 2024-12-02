using Ardalis.GuardClauses;

namespace RiverBooks.User.Data;

internal class UserStreetAddress
{
  public Guid Id { get; private set; }
  public string UserId { get; private set; } = string.Empty;
  public Address StreetAddress { get; private set; } = default!;

  public UserStreetAddress()
  {
    // EF
  }

  public UserStreetAddress(string userId, Address streetAddress)
  {
    UserId = Guard.Against.NullOrEmpty(userId);
    StreetAddress = Guard.Against.Null(streetAddress);
  }
}
