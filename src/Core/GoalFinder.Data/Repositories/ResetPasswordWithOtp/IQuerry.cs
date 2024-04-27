using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ResetPasswordWithOtp;

/// <summary>
/// IResetPasswordWithOtpRepository <see cref="IResetPasswordWithOtpRepository"/>
/// </summary>

public partial interface IResetPasswordWithOtpRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<Entities.UserToken> FindUserTokenByOtpCodeAsync(
        string otpCode,
        CancellationToken cancellationToken
    );
}
