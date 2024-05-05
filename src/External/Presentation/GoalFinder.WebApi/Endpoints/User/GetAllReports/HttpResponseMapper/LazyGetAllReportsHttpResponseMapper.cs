namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;

/// <summary>
///     Implementation for GetAllReports http response.
/// </summary>
internal static class LazyGetAllReportsHttpResponseMapper
{
    private static GetAllReportsHttpResponseMapper _GetAllReportsHttpResponseMapper;

    internal static GetAllReportsHttpResponseMapper Get()
    {
        return _GetAllReportsHttpResponseMapper ??= new();
    }

}
