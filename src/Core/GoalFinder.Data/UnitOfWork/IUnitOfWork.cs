using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using System.Threading;
using GoalFinder.Data.Repositories.RefreshToken;
using GoalFinder.Data.Repositories.UserDetail;

namespace GoalFinder.Data.UnitOfWork;

/// <summary>
///     Interface for unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Repository for managing the refresh token.
    /// </summary>
    IRefreshTokenRepository RefreshTokenRepository { get; }

    /// <summary>
    ///     Repository for managing the user.
    /// </summary>
    IUserDetailRepository UserDetailRepository { get; }

    /// <summary>
    ///     Create an execution strategy for managing the
    ///     db transaction which is initialized inside.
    /// </summary>
    /// <returns>
    ///     IExecutionStrategy for wrapping transaction inside.
    /// </returns>
    IExecutionStrategy CreateExecutionStrategy();

    /// <summary>
    ///     Create a database transaction.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task CreateTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Commit a database transaction.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Rollback the database transaction.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task RollBackTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Dispose the database transaction after done using
    /// </summary>
    /// <returns>
    ///     A value task containing result of operation.
    /// </returns>
    ValueTask DisposeTransactionAsync();

    /// <summary>
    ///     Save all entity changes to the database.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task SaveToDatabaseAsync(CancellationToken cancellationToken);
}
