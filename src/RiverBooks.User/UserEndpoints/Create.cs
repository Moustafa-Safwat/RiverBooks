using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RiverBooks.User.Data;

namespace RiverBooks.User.UserEndpoints;

internal class Create(
  UserManager<ApplicationUser> userManager,
  RoleManager<IdentityRole> roleManager,
  IUnitOfWork unitOfWork
  )
  : Endpoint<CreateUserRequest>
{
  public override void Configure()
  {
    Post("/user");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
  {
    var user = new ApplicationUser
    {
      UserName = req.UserName,
      Email = req.Email,
    };

    using (await unitOfWork.BeginTransactionAsync())
    {
      try
      {
        // Ensure the role exists before adding the user to the role
        if (await roleManager.RoleExistsAsync(req.Role))
        { 
          var result = await userManager.CreateAsync(user, req.Password);
          if (!result.Succeeded)
          {
            await unitOfWork.RollbackTransactionAsync();
            await SendErrorsAsync(cancellation: ct);
            return;
          }
          // Set the user's role
          var identityResult = await userManager.AddToRoleAsync(user, req.Role);
          if (!identityResult.Succeeded)
          {
            await unitOfWork.RollbackTransactionAsync();
            await SendErrorsAsync(cancellation: ct);
            return;
          }
          await unitOfWork.CommitTransactionAsync();
          await SendOkAsync(cancellation: ct);
          return;
        }

        await unitOfWork.RollbackTransactionAsync();
        await SendErrorsAsync(cancellation: ct);
        return;

      }
      catch (Exception)
      {
        await unitOfWork.RollbackTransactionAsync();
        await SendErrorsAsync(cancellation: ct);
        return;
      }
    }
  }
}
