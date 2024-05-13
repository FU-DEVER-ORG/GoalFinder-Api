namespace GoalFinder.Application.Features.Match.GetMatchesUpcoming;

/// <summary>
///     Extension Method for get matches upcoming features.
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
    public static string ToAppCode(this GetMatchesUpcomingResponseStatusCode statusCode)
    {
        return $"{nameof(Match)}.{nameof(GetMatchesUpcoming)}.{statusCode}";
    }
}
