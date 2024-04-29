using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;

/// <summary>
///    This is an auto generated class.
/// </summary>

internal partial class RefreshAccessTokenRepository
{
    /// <summary>
    ///    This is an auto generated class.
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _refreshTokens.AddAsync(
                entity: refreshToken,
                cancellationToken: cancellationToken
            );
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteRefreshTokenCommandAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );
                try
                {
                    await _refreshTokens
                        .Where(predicate: refreshToken =>
                            refreshToken.AccessTokenId == accessTokenId
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return executedTransactionResult;
    }
}
