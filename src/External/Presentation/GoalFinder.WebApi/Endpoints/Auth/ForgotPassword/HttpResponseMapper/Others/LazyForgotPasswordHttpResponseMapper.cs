using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;

internal static class LazyForgotPasswordHttpResponseMapper
{
    private static ForgotPasswordHttpResponseManager _forgotPasswordHttpResponseManager;
    internal static ForgotPasswordHttpResponseManager Get()
    {
        return _forgotPasswordHttpResponseManager ??= new();
    }
}
