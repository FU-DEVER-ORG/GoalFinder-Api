using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ReportUserAfterMatch;

internal partial class ReportUserAfterMatchRepository
{
    public async Task<bool> ReportUserAfterMatchCommandAsync(
        List<PlayerPrestigeScore> playerScores,
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
                    foreach (var playerScore in playerScores)
                    {
                        var userDetail = await _userDetails.FindAsync(playerScore.PlayerId);
                        if(userDetail != null) 
                        { 
                            userDetail.PrestigeScore += playerScore.bonusScore;
                        }
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
