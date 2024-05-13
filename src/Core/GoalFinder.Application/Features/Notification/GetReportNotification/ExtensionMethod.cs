namespace GoalFinder.Application.Features.Notification.GetReportNotification;

/// <summary>
///     Extension Method for get all notification report features.
/// </summary>
public static class ExtensionMethod
{
    /// <summary>
    ///     Mapping from feature response status code to
    ///     app code.
    /// </summary>
    /// <param name="statusCode">
    ///     Feature response status code
    /// </param>
    /// <returns>
    ///     New app code.
    /// </returns>
    public static string ToAppCode(this GetReportNotificationResponseStatusCode statusCode)
    {
        return $"{nameof(Match)}.{nameof(GetReportNotification)}.{statusCode}";
    }
}
