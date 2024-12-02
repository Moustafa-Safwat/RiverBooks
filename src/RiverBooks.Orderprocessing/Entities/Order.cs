using Ardalis.GuardClauses;

namespace RiverBooks.Orderprocessing.Entities;

/// <summary>
/// Represents an order placed by a user.
/// </summary>
internal class Order
{
  /// <summary>
  /// Gets the unique identifier for the order.
  /// </summary>
  public Guid Id { get; private set; }

  /// <summary>
  /// Gets the unique identifier for the user who placed the order.
  /// </summary>
  public Guid UserId { get; private set; }

  /// <summary>
  /// Gets the shipping address for the order.
  /// </summary>
  public Address ShippingAddress { get; private set; } = default!;

  /// <summary>
  /// Gets the billing address for the order.
  /// </summary>
  public Address BillingAddress { get; private set; } = default!;

  /// <summary>
  /// Gets the date and time when the order was created.
  /// </summary>
  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;

  private readonly IList<OrderItem> _orderItems = [];

  /// <summary>
  /// Gets the collection of items in the order.
  /// </summary>
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

  /// <summary>
  /// Adds an item to the order.
  /// </summary>
  /// <param name="item">The item to add to the order.</param>
  private void AddOrderItem(OrderItem item) => _orderItems.Add(Guard.Against.Null(item));

    /// <summary>
    /// Factory class for creating <see cref="Order"/> instances.
    /// </summary>
    internal class Factory
    {
        /// <summary>
        /// Creates a new <see cref="Order"/> instance.
        /// </summary>
        /// <param name="userId">The unique identifier for the user who placed the order.</param>
        /// <param name="shippingAddress">The shipping address for the order.</param>
        /// <param name="billingAddress">The billing address for the order.</param>
        /// <param name="items">The collection of items to include in the order.</param>
        /// <returns>A new <see cref="Order"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="shippingAddress"/>, <paramref name="billingAddress"/>, or any item in <paramref name="items"/> is null.</exception>
        public static Order Create(Guid userId, Address shippingAddress, Address billingAddress, IEnumerable<OrderItem> items)
        {
            var order = new Order
            {
                UserId = userId,
                ShippingAddress = Guard.Against.Null(shippingAddress),
                BillingAddress = Guard.Against.Null(billingAddress)
            };

            foreach (var item in items)
            {
                order.AddOrderItem(item);
            }

            return order;
        }
    }
}
