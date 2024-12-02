using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Orderprocessing.Entities;

namespace RiverBooks.Orderprocessing.Data;
/// <summary>
/// Configures the Order entity.
/// </summary>
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <summary>
    /// Configures the properties of the Order entity.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the Order entity.</param>
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);
        builder.ComplexProperty(order => order.ShippingAddress, SetAddressConfiguration());
        builder.ComplexProperty(order => order.BillingAddress, SetAddressConfiguration());
    }

    /// <summary>
    /// Sets the configuration for the Address complex property.
    /// </summary>
    /// <returns>An action to configure the Address complex property.</returns>
    private static Action<ComplexPropertyBuilder<Address>> SetAddressConfiguration()
    {
        return address =>
        {
            address.Property(a => a.Street1).HasMaxLength(100);
            address.Property(a => a.Street2).HasMaxLength(100);
            address.Property(a => a.City).HasMaxLength(100);
            address.Property(a => a.State).HasMaxLength(100);
            address.Property(a => a.PostalCode).HasMaxLength(100);
            address.Property(a => a.Country).HasMaxLength(100);
        };
    }
}
