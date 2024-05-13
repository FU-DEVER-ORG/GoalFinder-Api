namespace GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.HttpResponseMapper;

/// <summary>
///     Get report notification extension methods.
/// </summary>
internal static class LazyGetReportNotificationHttResponseMapper
{
    private static GetReportNotificationHttpResponseManager _GetReportNotificationHttpResponseManager;

    internal static GetReportNotificationHttpResponseManager Get()
    {
        return _GetReportNotificationHttpResponseManager ??= new();
    }
}
