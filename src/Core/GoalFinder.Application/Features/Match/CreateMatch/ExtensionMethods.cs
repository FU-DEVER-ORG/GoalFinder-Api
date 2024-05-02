namespace GoalFinder.Application.Features.Match.CreateMatch;

/// <summary>
///     Extension method for create match feature.
/// </summary>

public static class ExtensionMethods
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
    public static string ToAppCode(this CreateMatchResponseStatusCode statusCode)
    {
        return $"{nameof(Match)}.{nameof(CreateMatch)}.{statusCode}";
    }
}
