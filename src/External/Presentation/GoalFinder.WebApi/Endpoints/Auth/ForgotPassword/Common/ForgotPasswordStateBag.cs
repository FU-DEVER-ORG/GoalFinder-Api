namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;

/// <summary>
///     Represents the state bag used for the forgot password flow.
/// </summary>
internal sealed class ForgotPasswordStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
