using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ReportUserAfterMatch;

internal partial class ReportUserAfterMatchRepository
{
    public async Task<bool> ReportUserAfterMatchCommandAsync(
        int bonusAfterMatch,
        Guid playerId,
        CancellationToken ct
    )
    {
        var updateTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: ct
                );

                try
                {
                    var userDetails = await _userDetails.FindAsync(playerId);

                    if (userDetails != null)
                    {
                        userDetails.PrestigeScore += bonusAfterMatch;
                    }

                    await _context.SaveChangesAsync(cancellationToken: ct);
                    await dbTransaction.CommitAsync(cancellationToken: ct);

                    updateTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: ct);
                }
            });

        return updateTransactionResult;
    }
}
