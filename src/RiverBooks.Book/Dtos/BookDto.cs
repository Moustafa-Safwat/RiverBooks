namespace RiverBooks.Book.Dtos;

internal record BookDto(
    Guid Id,
    string Title,
    string Author,
    decimal Price,
    byte[]? RowVersion = null
);
