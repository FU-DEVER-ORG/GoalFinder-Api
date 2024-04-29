namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Common;

/// <summary>
/// State bag for refresh access token
/// </summary>
internal sealed class RefreshAccessTokenStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
