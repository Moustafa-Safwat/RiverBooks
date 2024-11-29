using RiverBooks.User.Data;

namespace RiverBooks.User.Repository;

internal interface IApplicationUserRepository
{
  Task<ApplicationUser?> GetApplicationUSerByEmailAsync(string email, CancellationToken cancellationToken);
  Task SaveChangesAsync(CancellationToken cancellationToken);
}
