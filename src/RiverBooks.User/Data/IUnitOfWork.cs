using Microsoft.EntityFrameworkCore.Storage;

namespace RiverBooks.User.Data;
/// <summary>
/// Represents a unit of work that encapsulates a series of operations 
/// to be executed within a single transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the transaction.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CommitTransactionAsync();

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RollbackTransactionAsync();
}
