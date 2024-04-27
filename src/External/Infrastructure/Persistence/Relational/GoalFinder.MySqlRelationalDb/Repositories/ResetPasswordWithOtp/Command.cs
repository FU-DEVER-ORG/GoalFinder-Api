using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ResetPasswordWithOtp;

/// <summary>
///     <see cref="ResetPasswordWithOtpRepository"/>
/// </summary>

internal sealed partial class ResetPasswordWithOtpRepository : IResetPasswordWithOtpRepository
{
    public async Task<bool> RemoveUserTokenUsingForResetPasswordAsync(
        string otpCode,
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
                    await _userTokens
                        .Where(predicate: userToken =>
                            userToken.Value == otpCode
                            && userToken.Name.Equals("PasswordResetOtpCode")
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
