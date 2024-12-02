namespace RiverBooks.Orderprocessing.Entities;

/// <summary>
/// Represents an item in an order.
/// </summary>
internal class OrderItem
{
    /// <summary>
    /// Gets the unique identifier for the order item.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the unique identifier for the book.
    /// </summary>
    public Guid BookId { get; private set; }

    /// <summary>
    /// Gets the quantity of the book ordered.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price of the book.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Gets the description of the order item.
    /// </summary>
    public string Description { get; private set; } = string.Empty;

  public OrderItem( )
  {
    // EF Core required
  }
  public OrderItem(Guid bookId, int quantity, decimal unitPrice, string description)
  {
    BookId = bookId;
    Quantity = quantity;
    UnitPrice = unitPrice;
    Description = description;
  }
}
