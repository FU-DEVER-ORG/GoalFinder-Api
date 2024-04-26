namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;

/// <summary>
///     GetAllMatches extension methods.
/// </summary>
internal static class LazyGetAllMatchesHttResponseMapper
{
    private static GetAllMatchesHttpResponseManager _GetAllMatchesHttpResponseManager;

    internal static GetAllMatchesHttpResponseManager Get()
    {
        return _GetAllMatchesHttpResponseManager ??= new();
    }
}
