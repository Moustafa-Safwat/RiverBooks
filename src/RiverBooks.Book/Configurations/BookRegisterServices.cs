using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Book.Data;
using RiverBooks.Book.Repository;
using RiverBooks.Book.Services;
using RiverBooks.Repository;

namespace RiverBooks.Book.Configurations;

public static class BookRegisterServices
{
  public static IServiceCollection RegisterBookServices(this IServiceCollection service,
    ConfigurationManager configuration,
    IList<System.Reflection.Assembly> assemblies)
  {
    service.AddScoped<IBookRepository, EFBookRepository>();
    service.AddScoped<IBookService, BookService>();
    service.AddDbContext<BookDbContext>(options =>
    {
      options.UseSqlServer(configuration.GetConnectionString("RiverBooksCS"));
    });
    assemblies.Add(typeof(BookRegisterServices).Assembly);
    return service;
  }
}
