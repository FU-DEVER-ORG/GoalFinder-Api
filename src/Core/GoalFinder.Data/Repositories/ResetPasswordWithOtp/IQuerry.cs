using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ResetPasswordWithOtp;

/// <summary>
/// IResetPasswordWithOtpRepository <see cref="IResetPasswordWithOtpRepository"/>
/// </summary>

public partial interface IResetPasswordWithOtpRepository
{
    Task<bool> IsOtpCodeForResettingPasswordExpiredAsync(
        string otpCode,
        CancellationToken cancellationToken
    );
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
    Task<GoalFinder.Data.Entities.UserToken> FindUserTokenByOtpCodeAsync(
        string otpCode,
        CancellationToken cancellationToken
    );
}
