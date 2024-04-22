namespace GoalFinder.WebApi.Endpoints.Auth.Login.Common;

/// <summary>
///     Represents the login state bag.
/// </summary>
internal sealed class LoginStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal int HttpCode { get; set; }
}
