using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.CreateMatch;

internal sealed partial class CreateMatchRepository
{
    public async Task<bool> CreateMatchCommandAsync(
        Guid userId,
        FootballMatch footballMatch,
        CancellationToken cancellationToken
    )
    {
        var createTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    _footballMatches.Add(footballMatch);

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);
                    createTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return createTransactionResult;
    }
}
