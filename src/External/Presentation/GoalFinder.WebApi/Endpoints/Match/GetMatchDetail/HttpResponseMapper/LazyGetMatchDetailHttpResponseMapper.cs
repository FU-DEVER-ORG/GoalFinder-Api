namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;

/// <summary>
///     Lazy Get Match Detail Http Response Mapper
/// </summary>

internal static class LazyGetMatchDetailHttpResponseMapper
{
    private static GetMatchDetailHttpResponseManager _GetMatchDetailHttpResponseMapper;

    internal static GetMatchDetailHttpResponseManager Get()
    {
        return _GetMatchDetailHttpResponseMapper ??= new();
    }
}
