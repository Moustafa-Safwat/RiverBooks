using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Book;

public static class BookRegisterServices
{
    public static IServiceCollection RegisterBookServices(this IServiceCollection service)
    {
        service.AddScoped<IBookService, BookService>();
        return service;
    }
}
