using Microsoft.EntityFrameworkCore;
namespace RiverBooks.Book.Data;

using RiverBooks.Models;
using System.Reflection;
/// <summary>
/// Represents the database context for the Book entity.
/// </summary>
public sealed class BookDbContext(DbContextOptions<BookDbContext> options)
  : DbContext(options)
{
  /// <summary>
  /// Gets or sets the Books DbSet.
  /// </summary>
  internal DbSet<Book> Books => Set<Book>();

  /// <summary>
  /// Configures the schema needed for the Book entity.
  /// </summary>
  /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(DbSchemas.BOOKS);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  /// <summary>
  /// Configures the conventions for the model.
  /// </summary>
  /// <param name="configurationBuilder">The builder used to configure conventions for the model.</param>
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
  }
}
