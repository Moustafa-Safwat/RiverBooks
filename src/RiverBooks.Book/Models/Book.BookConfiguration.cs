using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Book.Data;
namespace RiverBooks.Models;
internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
  public void Configure(EntityTypeBuilder<Book> builder)
  {
    builder.HasKey(book => book.Id);

    builder.Property(book => book.Author)
      .IsRequired()
      .HasMaxLength(DbEntitiesConfigurations.DEFAULT_MAX_LENGTH);

    builder.Property(book => book.Title)
      .IsRequired()
      .HasMaxLength(DbEntitiesConfigurations.DEFAULT_MAX_LENGTH);

    builder.Property(book => book.RowVersion)
      .IsConcurrencyToken();

    builder.HasData(SeedBookData());

    // Add Constraints
    builder.ToTable(book => book.HasCheckConstraint("Ck_Price_Positive",
      $"[{nameof(Book.Price)}] >= 0"));
  }

  private IEnumerable<Book> SeedBookData()
  {
    yield return new(new Guid("A89F6CD7-4693-457B-9009-02205DBBFE45"), "The Great Gatsby", "F. Scott Fitzgerald", 10.99m, []);
    yield return new(new Guid("E4FA19BF-6981-4E50-A542-7C9B26E9EC31"), "1984", "George Orwell", 8.99m,[]);
    yield return new(new Guid("17C61E41-3953-42CD-8F88-D3F698869B35"), "To Kill a Mockingbird", "Harper Lee", 12.99m,[]);
    yield return new(new Guid("E4FA19BF-6981-4E50-A542-7C9B26E9EC34"), "Pride and Prejudice", "Jane Austen", 9.99m,[]);
    yield return new(new Guid("17C61E41-3953-42CD-8F88-D3F698869B38"), "The Catcher in the Rye", "J.D. Salinger", 11.99m,[]);
  }
}
