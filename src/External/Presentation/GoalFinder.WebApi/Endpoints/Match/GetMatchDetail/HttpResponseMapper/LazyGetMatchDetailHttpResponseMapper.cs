namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;

internal static class LazyGetMatchDetailHttpResponseMapper
{
    private static GetMatchDetailHttpResponseManager _GetMatchDetailHttpResponseMapper;

    internal static GetMatchDetailHttpResponseManager Get()
    {
        return _GetMatchDetailHttpResponseMapper ??= new();
    }
}
