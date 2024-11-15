using Ardalis.GuardClauses;
using RiverBooks.Book.Models;

#pragma warning disable IDE0130
namespace RiverBooks.Models;
#pragma warning restore IDE0130

#pragma warning disable IDE0290
/// <summary>
/// Represents a book with an ID, title, author, and price.
/// </summary>
internal class Book : BaseEntity
{
  /// <summary>
  /// Gets the title of the book.
  /// </summary>
  public string Title { get; private set; }

  /// <summary>
  /// Gets the author of the book.
  /// </summary>
  public string Author { get; private set; }

  /// <summary>
  /// Gets the price of the book.
  /// </summary>
  public decimal Price { get; private set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="Book"/> class.
  /// </summary>
  /// <param name="id">The unique identifier for the book.</param>
  /// <param name="title">The title of the book.</param>
  /// <param name="author">The author of the book.</param>
  /// <param name="price">The price of the book.</param>
  public Book(Guid id, string title, string author, decimal price, byte[] rowVersion)
  {
    Id = Guard.Against.Default(id);
    Title = ValidateTitle(title);
    Author = ValidateAuthor(author);
    Price = ValidatePrice(price);
    RowVersion = Guard.Against.Null(rowVersion);
  }

  /// <summary>
  /// Updates the price of the book.
  /// </summary>
  /// <param name="price">The new price of the book.</param>
  public void UpdatePrice(decimal price)
  {
    Price = ValidatePrice(price);
  }

  /// <summary>
  /// Updates the title of the book.
  /// </summary>
  /// <param name="title">The new title of the book.</param>
  public void UpdateTitle(string title)
  {
    Title = ValidateTitle(title);
  }

  /// <summary>
  /// Updates the author of the book.
  /// </summary>
  /// <param name="author">The new author of the book.</param>
  public void UpdateAuthor(string author)
  {
    Author = ValidateAuthor(author);
  }

  #region Validation methods

  /// <summary>
  /// Validates the price of the book.
  /// </summary>
  /// <param name="price">The price to validate.</param>
  /// <returns>The validated price.</returns>
  private static decimal ValidatePrice(decimal price)
  {
    return Guard.Against.Negative(price);
  }

  /// <summary>
  /// Validates the title of the book.
  /// </summary>
  /// <param name="title">The title to validate.</param>
  /// <returns>The validated title.</returns>
  private static string ValidateTitle(string title)
  {
    return Guard.Against.NullOrEmpty(title);
  }

  /// <summary>
  /// Validates the author of the book.
  /// </summary>
  /// <param name="author">The author to validate.</param>
  /// <returns>The validated author.</returns>
  private static string ValidateAuthor(string author)
  {
    return Guard.Against.NullOrEmpty(author);
  }

  #endregion
}
#pragma warning restore IDE0290
