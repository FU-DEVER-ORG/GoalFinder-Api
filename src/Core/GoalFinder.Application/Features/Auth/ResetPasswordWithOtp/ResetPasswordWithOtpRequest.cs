using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
///     Request for reset password with otp
/// </summary>
/// <param name="OTPCode"></param>

public sealed class ResetPasswordWithOtpRequest : IFeatureRequest<ResetPasswordWithOtpResponse>
{
    public string OtpCode { get; set; }
    public string newPassword { get; set; }
    public string confirmPassword { get; set; }
}
