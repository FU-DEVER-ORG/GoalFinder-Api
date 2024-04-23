namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;

internal sealed class ForgotPasswordStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
