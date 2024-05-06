namespace GoalFinder.Application.Features.Match.GetMatchDetail;

/// <summary>
///    Extension Method
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

    public static string ToAppCode(this GetMatchDetailResponseStatusCode statusCode)
    {
        return $"{nameof(Match)}.{nameof(GetMatchDetail)}.{statusCode}";
    }
}
