using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Book.Repository;
using RiverBooks.Book.Services;
using RiverBooks.Repository;

namespace RiverBooks.Book.Configurations;

public static class BookRegisterServices
{
  public static IServiceCollection RegisterBookServices(this IServiceCollection service)
  {
    service.AddScoped<IBookRepository, BookRepository>();
    service.AddScoped<IBookService, BookService>();
    return service;
  }
}
