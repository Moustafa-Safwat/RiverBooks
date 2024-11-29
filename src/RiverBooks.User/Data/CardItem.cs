using Ardalis.GuardClauses;

namespace RiverBooks.User.Data;
/// <summary>
/// Represents an item in the user's card.
/// </summary>
internal class CardItem
{
  /// <summary>
  /// Gets the unique identifier for the card item.
  /// </summary>
  public Guid Id { get; private set; }

  /// <summary>
  /// Gets the unique identifier for the associated book.
  /// </summary>
  public Guid BookId { get; private set; }

  /// <summary>
  /// Gets the description of the card item.
  /// </summary>
  public string Description { get; private set; } = string.Empty;

  /// <summary>
  /// Gets the price of the card item.
  /// </summary>
  public decimal Price { get; private set; }

  /// <summary>
  /// Gets the quantity of the card item.
  /// </summary>
  public int Quantity { get; private set; }

  /// <summary>
  /// Gets the user associated with the card item.
  /// </summary>
  public ApplicationUser User { get; private set; } = null!;

  public CardItem(Guid id, Guid bookId, string description, decimal price, int count)
  {
    Id = Guard.Against.Default(id);
    BookId = Guard.Against.Default(bookId);
    Description = Guard.Against.NullOrEmpty(description);
    Price = Guard.Against.Negative(price);
    Quantity = Guard.Against.NegativeOrZero(count);
  }

  public CardItem()
  {
    // EF Core
  }

  internal void UpdateQuantity(int quantity)
  {
    Quantity = Guard.Against.NegativeOrZero(quantity);
  }

  internal void UpdateDescription(string description)
  {
    Description = Guard.Against.NullOrEmpty(description);
  }

  internal void UpdatePrice(decimal price)
  {
    Price = Guard.Against.Negative(price);
  }
}
