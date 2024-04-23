using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;

/// <summary>
/// Implementation of <see cref="IForgotPasswordRepository"/>
/// </summary>
internal sealed partial class ForgotPasswordRepository
{
    /// <summary>
    ///     Add Reset Password Token To Database
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="passwordResetOtpCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> AddResetPasswordTokenToDatabaseAsync(
        Guid userId, 
        string passwordResetOtpCode, 
        CancellationToken cancellationToken)
    {
        //Create User Token
        UserToken userToken = new UserToken()
        {
            UserId = userId,
            LoginProvider = Guid.NewGuid().ToString(),
            Name = "PasswordResetOtpCode",
            Value = passwordResetOtpCode
        };
        //Check if User Token is default
        if(Equals(objA: userToken, objB: default))
        {
            return false;
        }
        var executedTransactionResult = false;
        //Create Execution Strategy
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
                    //Save Changes
                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);
                    executedTransactionResult = true;
                }
                catch
                {
                    //Rollback Transaction
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
