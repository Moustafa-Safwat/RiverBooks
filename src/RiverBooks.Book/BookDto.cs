namespace RiverBooks.Book;

internal record BookDto(
    Guid Id,
    string Title,
    string Author,
    decimal Price
    );
