using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RiverBooks.User.Data;

namespace RiverBooks.User.UserEndpoints;
internal class Login(UserManager<ApplicationUser> userManager
  ,IConfiguration configuration)
  : Endpoint<UserLoginRequest, TokenDto>
{
  public override void Configure()
  {
    Post("/user/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
  {
    var user = await userManager.FindByEmailAsync(req.Email);
    if (user is null)
    {
      await SendErrorsAsync();
      return;
    }

    var loginSuccessful = await userManager.CheckPasswordAsync(user, req.Password);
    if (!loginSuccessful)
    {
      await SendErrorsAsync();
      return;
    }

    var userRole = await userManager.GetRolesAsync(user);

    var jwtSecret = Config["Auth:JwtSecret"];
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName!),
        new Claim(ClaimTypes.Email, user.Email!),
        // Add more claims as needed
    };

    var token = JwtBearer.CreateToken(options =>
    {
      options.SigningKey = jwtSecret!;
      options.ExpireAt = DateTime.Now.AddMinutes(15);
      options.Issuer = configuration["Issuer"];
      options.User.Claims.AddRange(claims);
      options.User.Roles.AddRange(userRole);
    });
    await SendOkAsync(new TokenDto(token));
  }
}
