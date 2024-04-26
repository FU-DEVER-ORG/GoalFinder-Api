namespace GoalFinder.Application.Features.Match.GetAllMatches;

/// <summary>
///     Extension Method for get all football matches features.
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
    public static string ToAppCode(this GetAllMatchesResponseStatusCode statusCode)
    {
        return $"{nameof(Match)}.{nameof(GetAllMatches)}.{statusCode}";
    }
}
