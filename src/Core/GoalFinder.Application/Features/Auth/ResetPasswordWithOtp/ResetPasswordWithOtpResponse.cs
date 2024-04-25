using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
/// Response for reset password with otp
/// </summary>

public sealed class ResetPasswordWithOtpResponse : IFeatureResponse
{
    public ResetPasswordWithOtpResponseStatusCode StatusCode { get; init; }
}
