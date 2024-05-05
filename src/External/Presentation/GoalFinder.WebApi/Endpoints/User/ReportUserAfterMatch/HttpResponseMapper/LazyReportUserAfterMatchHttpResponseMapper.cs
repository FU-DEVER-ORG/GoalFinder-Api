namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;

internal static class LazyReportUserAfterMatchHttpResponseMapper
{
    private static ReportUserAfterMatchHttpResponseManager _reportUserAfterMatchHttpResponseManager;

    internal static ReportUserAfterMatchHttpResponseManager Get()
    {
        return _reportUserAfterMatchHttpResponseManager ??= new();
    }
}
