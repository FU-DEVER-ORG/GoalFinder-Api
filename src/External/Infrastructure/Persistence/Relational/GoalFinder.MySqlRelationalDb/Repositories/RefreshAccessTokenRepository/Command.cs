using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;

/// <summary>
///    This is an auto generated class.
/// </summary>

internal partial class RefreshAccessTokenRepository
{
    public async Task<bool> UpdateRefreshTokenCommandAsync(
        Guid accessTokenId,
        string refreshTokenValue,
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
                        .ExecuteUpdateAsync(
                            setPropertyCalls: builder =>
                                builder
                                    .SetProperty(
                                        refreshToken => refreshToken.AccessTokenId,
                                        accessTokenId
                                    )
                                    .SetProperty(
                                        refreshToken => refreshToken.RefreshTokenValue,
                                        refreshTokenValue
                                    ),
                            cancellationToken: cancellationToken
                        );

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
