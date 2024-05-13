using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ReportUserAfterMatch;

internal partial class ReportUserAfterMatchRepository
{
    public async Task<List<UserDetail>> ReportUserAfterMatchCommandAsync(
        List<PlayerPrestigeScore> playerScores,
        CancellationToken ct
    )
    {
        var updateTransactionResult = false;

        List<UserDetail> updatedUsers = new List<UserDetail>();

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
                            userDetail.PrestigeScore += playerScore.BonusScore;
                            updatedUsers.Add(userDetail);
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken: ct);
                    await dbTransaction.CommitAsync(cancellationToken: ct);

                    updateTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: ct);
                    // Clear list if failed
                    updatedUsers.Clear();
                }
            });

        return updatedUsers;
    }
}
