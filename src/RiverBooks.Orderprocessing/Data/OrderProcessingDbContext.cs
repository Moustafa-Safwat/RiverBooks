using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Orderprocessing.Data;
/// <summary>
/// Represents the database context for order processing.
/// </summary>
internal class OrderProcessingDbContext(
  DbContextOptions<OrderProcessingDbContext> options
  )
  : DbContext(options)
{
    /// <summary>
    /// Configures the schema needed for the database context.
    /// </summary>
    /// <param name="builder">The model builder used to configure the schema.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("OrderProcessing");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    /// <summary>
    /// Configures the conventions for the database context.
    /// </summary>
    /// <param name="configuration">The model configuration builder used to configure conventions.</param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<decimal>().HavePrecision(18, 2);
    }
}
