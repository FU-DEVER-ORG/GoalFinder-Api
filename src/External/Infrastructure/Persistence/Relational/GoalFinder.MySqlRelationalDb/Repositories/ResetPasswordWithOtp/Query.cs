using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ResetPasswordWithOtp;

/// <summary>
///     <see cref="ResetPasswordWithOtpRepository"/>
/// </summary>

internal sealed partial class ResetPasswordWithOtpRepository : IResetPasswordWithOtpRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue,
            cancellationToken: cancellationToken
        );
    }

    public Task<UserToken> FindUserTokenByOtpCodeAsync(
        string otpCode,
        CancellationToken cancellationToken
    )
    {
        return _userTokens
            .Where(userToken => userToken.LoginProvider == otpCode)
            .Select(userToken => new UserToken
            {
                ExpiredAt = userToken.ExpiredAt,
                Value = userToken.Value,
                UserId = userToken.UserId
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
