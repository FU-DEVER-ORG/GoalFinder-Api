using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.ResetPasswordWithOtp;

/// <summary>
/// <see cref="ResetPasswordWithOtpRepository"/>
/// </summary>

internal sealed partial class ResetPasswordWithOtpRepository : IResetPasswordWithOtpRepository
{
    public Task<bool> IsOtpCodeForResettingPasswordExpiredAsync(string otpCode, CancellationToken cancellationToken)
    {
        return _userTokens
            .AnyAsync(predicate:
                userToken => userToken.ExpiredAt < DateTime.UtcNow,
                cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _userDetails
            .AnyAsync(predicate:
                userDetail => userDetail.UserId == userId &&
                userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
                userDetail.RemovedAt != DateTime.MinValue,
                cancellationToken: cancellationToken);
    }

    public Task<UserToken> FindUserTokenByOtpCodeAsync(
        string otpCode,
        CancellationToken cancellationToken)
    {
        return _userTokens
                .Where(userToken => userToken.LoginProvider == otpCode)
                .Select(userToken => new UserToken
                {
                    LoginProvider = userToken.LoginProvider,
                    ExpiredAt = userToken.ExpiredAt,
                    Name = userToken.Name,
                    Value = userToken.Value,
                    UserId = userToken.UserId
                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}