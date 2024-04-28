namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Common;

internal sealed class RefreshAccessTokenStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
