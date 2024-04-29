namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch.HttpResponseMapper;

internal static class LazyCreateMatchHttpResponseMapper
{
    private static CreateMatchHttpResponseManager _CreateMatchHttpResponseManager;

    internal static CreateMatchHttpResponseManager Get()
    {
        return _CreateMatchHttpResponseManager ??= new();
    }
}
