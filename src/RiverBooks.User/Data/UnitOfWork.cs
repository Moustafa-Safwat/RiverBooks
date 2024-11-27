using Microsoft.EntityFrameworkCore.Storage;

namespace RiverBooks.User.Data;
/// <summary>
/// Represents a unit of work that encapsulates a transaction for the UserDbContext.
/// </summary>
/// <param name="userDbContext">The UserDbContext instance to be used for the transaction.</param>
internal class UnitOfWork(UserDbContext userDbContext) : IUnitOfWork
{
    private IDbContextTransaction _transaction = null!;

    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the started transaction.</returns>
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _transaction = await userDbContext.Database.BeginTransactionAsync();
        return _transaction;
    }

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
    }

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
    }

    /// <summary>
    /// Disposes the current transaction.
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
