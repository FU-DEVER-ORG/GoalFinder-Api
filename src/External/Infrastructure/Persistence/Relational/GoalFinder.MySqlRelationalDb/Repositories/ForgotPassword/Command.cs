using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;

/// <summary>
///     Implementation of <see cref="IForgotPasswordRepository"/>
/// </summary>
internal sealed partial class ForgotPasswordRepository
{
    public async Task<bool> AddResetPasswordTokenToDatabaseAsync(
        Guid userId,
        string otpId,
        string otpValue,
        CancellationToken cancellationToken
    )
    {
        UserToken userToken =
            new()
            {
                UserId = userId,
                LoginProvider = otpId,
                Name = "PasswordResetOtpCode",
                Value = otpValue,
                ExpiredAt = DateTime.UtcNow.AddMinutes(1)
            };

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
                    //Remove old user OTP code
                    await _userTokens
                        .Where(predicate: userToken =>
                            userToken.UserId == userId
                            && userToken.Name.Equals("PasswordResetOtpCode")
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    //Add new user OTP code
                    await _userTokens.AddAsync(
                        entity: userToken,
                        cancellationToken: cancellationToken
                    );
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
