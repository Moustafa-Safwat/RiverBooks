using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Orderprocessing.Data;
using RiverBooks.Orderprocessing.Repository;

namespace RiverBooks.Orderprocessing.Configurations;
/// <summary>
/// Provides extension methods to register services for the order processing module.
/// </summary>
public static class OrderprocessingRegisterServices
{
  /// <summary>
  /// Registers the services required for order processing.
  /// </summary>
  /// <param name="services">The service collection to which the services will be added.</param>
  /// <param name="configuration">The configuration manager to retrieve connection strings and other settings.</param>
  /// <param name="assemblies">The list of assemblies to which the current assembly will be added.</param>
  /// <returns>The updated service collection.</returns>
  public static IServiceCollection RegisterOrderprocessingrServices(this IServiceCollection services,
   ConfigurationManager configuration,
   IList<System.Reflection.Assembly> assemblies)
  {
    services.AddDbContext<OrderProcessingDbContext>(options =>
    {
      options.UseSqlServer(configuration.GetConnectionString("RiverBooksOrderProcessingCS"));
    });

    services.AddScoped<IOrderRepository, EFOrderRepository>();
    assemblies.Add(typeof(OrderprocessingRegisterServices).Assembly);
    return services;
  }
}
