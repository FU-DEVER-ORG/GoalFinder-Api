namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;

/// <summary>
///     Lazy initialization of <see cref="ResetPasswordWithOtpHttpResponseManager"/>
/// </summary>

internal static class LazyResetPasswordWithOtpHttpResponseMapper
{
    private static ResetPasswordWithOtpHttpResponseManager
                    _resetPasswordWithOtpHttpResponseManager;
    internal static ResetPasswordWithOtpHttpResponseManager Get()
    {
        return _resetPasswordWithOtpHttpResponseManager ??= new();
    }
}
