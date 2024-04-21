using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Text.Json;

namespace GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;

internal partial class InsertErrorLogRepository
{
    public async Task<bool> InsertErrorLogCommandAsync(
        Exception exception,
        CancellationToken cancellationToken)
    {
        var executedTransactionResult = false;

        await _context.Database
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken);

                try
                {
                    ErrorLogging errorLogging = new()
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        ErrorMessage = exception.Message,
                        ErrorStackTrace = exception.StackTrace,
                        Data = JsonSerializer.Serialize(exception.Data)
                    };

                    await _context
                        .Set<ErrorLogging>()
                        .AddAsync(
                            entity: errorLogging,
                            cancellationToken: cancellationToken);

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
