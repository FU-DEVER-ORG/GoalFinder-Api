namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;

internal static class LazyRefreshAccessTokenHttpResponseMapper
{
    private static RefreshAccessTokenHttpResponseManager _refreshAccessTokenHttpResponseManager;

    internal static RefreshAccessTokenHttpResponseManager Get()
    {
        return _refreshAccessTokenHttpResponseManager ??= new();
    }
}
