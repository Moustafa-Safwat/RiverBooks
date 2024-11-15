using System.ComponentModel.DataAnnotations;

namespace RiverBooks.Book.Models;
internal abstract class BaseEntity
{
  /// <summary>
  /// Gets the unique identifier for the book.
  /// </summary>
  public Guid Id { get; protected set; }

  /// <summary>
  /// Gets the row version which is used for concurrency control.
  /// </summary>
  [Timestamp]
  public byte[] RowVersion { get; protected set; } = [];
}
