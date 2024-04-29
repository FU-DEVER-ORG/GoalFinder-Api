using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ResetPasswordWithOtp;

/// <summary>
/// IResetPasswordWithOtpRepository <see cref="IResetPasswordWithOtpRepository"/>
/// </summary>
public partial interface IResetPasswordWithOtpRepository
{
    Task<bool> RemoveUserTokenUsingForResetPasswordAsync(
        string otpCode,
        CancellationToken cancellationToken
    );
}
