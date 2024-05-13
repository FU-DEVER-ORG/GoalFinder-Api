namespace GoalFinder.Application.Features.User.GetAllReports;

/// <summary>
///     Extension Method for get all reports
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

    public static string ToAppCode(this GetAllReportsStatusCode statusCode)
    {
        return $"{nameof(User)}.{nameof(GetAllReports)}.{statusCode}";
    }
}
