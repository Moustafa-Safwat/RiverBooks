using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace RiverBooks.User.Data;
/// <summary>
/// Represents the database context for user-related data, inheriting from IdentityDbContext.
/// </summary>
internal sealed class UserDbContext(DbContextOptions<UserDbContext> options)
  : IdentityDbContext<ApplicationUser>(options)
{

  /// <summary>
  /// Configures the schema needed for the identity framework and applies configurations from the current assembly.
  /// </summary>
  /// <param name="builder">The model builder used to configure the entity framework model.</param>
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema(DbSchemas.USER);
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
    SetRolesInDb(builder);
  }

  /// <summary>
  /// Seeds the database with predefined roles.
  /// </summary>
  /// <param name="builder">The model builder used to configure the entity framework model.</param>
  private void SetRolesInDb(ModelBuilder builder)
  {
    builder.Entity<IdentityRole>().HasData(
      new IdentityRole { Id = "1245222e-0adf-4218-bf77-1a2a31010e65", Name = "Admin", NormalizedName = "ADMIN" },
      new IdentityRole { Id = "e7b56b86-d8ec-4eea-bc19-d73bca66481f", Name = "User", NormalizedName = "USER" }
    );
  }

  /// <summary>
  /// Configures conventions for the model, such as setting the precision for decimal properties.
  /// </summary>
  /// <param name="configurationBuilder">The model configuration builder used to configure conventions.</param>
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
  }
}
