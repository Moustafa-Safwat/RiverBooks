using RiverBooks.Book.Dtos;

namespace RiverBooks.Book;

internal record GetBooksDto(IList<BookDto> Books);
