namespace RiverBooks.Book.Services;

internal class BookService : IBookService
{
  public GetBooksDto GetAllBooks()
  {
    return new([
        new BookDto(Guid.NewGuid(), "Book Title 1", "Author 1", 19.99m),
            new BookDto(Guid.NewGuid(), "Book Title 2", "Author 2", 29.99m),
            new BookDto(Guid.NewGuid(), "Book Title 3", "Author 3", 39.99m)
    ]);
  }
}
