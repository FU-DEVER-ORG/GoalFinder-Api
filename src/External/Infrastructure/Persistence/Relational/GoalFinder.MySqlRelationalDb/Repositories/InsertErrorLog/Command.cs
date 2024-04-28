using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;

internal partial class InsertErrorLogRepository
{
    public async Task<bool> InsertErrorLogCommandAsync(
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        try
        {
            ErrorLogging errorLogging =
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    ErrorMessage = exception.Message,
                    ErrorStackTrace = exception.StackTrace,
                    Data = JsonSerializer.Serialize(value: exception.Data)
                };

            await _context
                .Set<ErrorLogging>()
                .AddAsync(entity: errorLogging, cancellationToken: cancellationToken);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
