namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;

/// <summary>
///     Implementation for GetAllReports http response.
/// </summary>
internal static class LazyGetAllReportsHttpResponseMapper
{
    private static GetAllReportsHttpResponseManager _GetAllReportsHttpResponseMapper;

    internal static GetAllReportsHttpResponseManager Get()
    {
        return _GetAllReportsHttpResponseMapper ??= new();
    }
}
