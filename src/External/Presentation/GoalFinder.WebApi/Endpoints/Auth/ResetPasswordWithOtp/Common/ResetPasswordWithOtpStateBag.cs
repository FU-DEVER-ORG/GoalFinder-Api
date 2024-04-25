namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Common;

/// <summary>
/// State bag for reset password with otp for cache
/// </summary>

internal sealed class ResetPasswordWithOtpStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
