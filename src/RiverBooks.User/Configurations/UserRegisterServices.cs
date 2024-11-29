using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.User.Data;
using RiverBooks.User.Repository;

namespace RiverBooks.User.Configurations;
public static class UserRegisterServices
{
  public static IServiceCollection RegisterUserServices(this IServiceCollection services,
    ConfigurationManager configuration,
    IList<System.Reflection.Assembly> assemblies)
  {
    services.AddDbContext<UserDbContext>(options =>
    {
      options.UseSqlServer(configuration.GetConnectionString("RiverBooksUsersCS"));
    });

    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
      options.User.RequireUniqueEmail = true;

      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireUppercase = true;

      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
      options.Lockout.MaxFailedAccessAttempts = 3;
    })
      .AddEntityFrameworkStores<UserDbContext>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IApplicationUserRepository, EFApplicationUserRepository>();
    assemblies.Add(typeof(UserRegisterServices).Assembly);
    return services;
  }
}
