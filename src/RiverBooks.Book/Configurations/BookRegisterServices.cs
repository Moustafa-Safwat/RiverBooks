using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Book.Services;

namespace RiverBooks.Book.Configurations;

public static class BookRegisterServices
{
  public static IServiceCollection RegisterBookServices(this IServiceCollection service)
  {
    service.AddScoped<IBookService, BookService>();
    return service;
  }
}
