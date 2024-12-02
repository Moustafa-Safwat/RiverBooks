using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.User.Data;

internal class ApplicationUser : IdentityUser
{
  public string FullName { get; private set; } = string.Empty;
  private readonly IList<CardItem> _cardItems = [];
  private readonly IList<UserStreetAddress> _userStreetAddresses = [];
  // Navigation Property
  public IReadOnlyCollection<CardItem> CardItems => _cardItems.ToList();
  public IReadOnlyCollection<UserStreetAddress> UserStreetAddresses => _userStreetAddresses.ToList();

  public void AddCardItem(CardItem item)
  {
    Guard.Against.Null(item);
    var existingBook = _cardItems.FirstOrDefault(ci => ci.BookId == item.BookId);
    if (existingBook != null)
    {
      existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);
      existingBook.UpdateDescription(item.Description); // to update any change in description
      existingBook.UpdatePrice(item.Price); // to update any change in price
      return;
    }
    _cardItems.Add(item);
  }

  internal void ClearCart()
  {
    _cardItems.Clear();
  }

  internal UserStreetAddress AddAddress(Address address)
  {
    Guard.Against.Null(address);

    var existingAddress = _userStreetAddresses.FirstOrDefault(usa => usa.StreetAddress == address);
    if (existingAddress is not null)
    {
      return existingAddress;
    }
    var newAddress = new UserStreetAddress(Id, address);
    _userStreetAddresses.Add(newAddress);

    return newAddress;
  }
}
