using Microsoft.EntityFrameworkCore;
using RiverBooks.User.Data;

namespace RiverBooks.User.Repository;
internal class EFApplicationUserRepository(
  UserDbContext dbContext
  )
  : IApplicationUserRepository
{
  public async Task<ApplicationUser?> GetApplicationUSerByEmailAsync(string email, CancellationToken cancellationToken)
  {
    return await dbContext.Users
      .Include(user => user.CardItems)
      .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

  }

  public async Task<ApplicationUser?> GetApplicationUserWithAddressByEmailAsync(string email, CancellationToken cancellationToken)
  {
    return await dbContext.Users
      .Include(user => user.UserStreetAddresses)
      .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
  }

  public async Task SaveChangesAsync(CancellationToken cancellationToken)
  {
    await dbContext.SaveChangesAsync(cancellationToken);
  }
}
