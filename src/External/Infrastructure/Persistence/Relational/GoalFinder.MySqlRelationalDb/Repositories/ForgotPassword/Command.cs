using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;


internal sealed partial class ForgotPasswordRepository
{
    public async Task<bool> AddResetPasswordTokenToDatabaseAsync(
        Guid userId, 
        string passwordResetOtpCode, 
        CancellationToken cancellationToken)
    {
        UserToken userToken = new UserToken()
        {
            UserId = userId,
            LoginProvider = Guid.NewGuid().ToString(),
            Name = "PasswordResetOtpCode",
            Value = passwordResetOtpCode
        };
        if(Equals(objA: userToken, objB: default))
        {
            return false;
        }
        var executedTransactionResult = false;

        await _context.Database
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken);
                try
                {
                    //Remove old user OTP code
                    var foundUserTokens = await _userTokens
                        .Where(
                            predicate: userToken => userToken.UserId == userId 
                            && userToken.Name == "PasswordResetOtpCode")
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
