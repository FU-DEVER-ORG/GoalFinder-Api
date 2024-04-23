namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;

/// <summary>
/// Forgot password extension method
/// </summary>
internal static class LazyForgotPasswordHttpResponseMapper
{
    /// <summary>
    /// Forgot password http response manager
    /// </summary>
    private static ForgotPasswordHttpResponseManager _forgotPasswordHttpResponseManager;
    internal static ForgotPasswordHttpResponseManager Get()
    {
        return _forgotPasswordHttpResponseManager ??= new();
    }
}
