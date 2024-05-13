namespace GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.HttpResponseMapper;

internal static class LazyGetMatchesUpcomingHttpResponseMapper
{
    private static GetMatchesUpcomingHttpResponseManager _updateUserInfoHttpResponseManager;

    internal static GetMatchesUpcomingHttpResponseManager Get()
    {
        return _updateUserInfoHttpResponseManager ??= new();
    }
}
