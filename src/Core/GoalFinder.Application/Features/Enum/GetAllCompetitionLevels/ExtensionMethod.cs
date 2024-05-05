namespace GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;

/// <summary>
///     Extension Method for get all competitionLevels features.
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
    public static string ToAppCode(this GetAllCompetitionLevelsResponseStatusCode statusCode)
    {
        return $"{nameof(Enum)}.{nameof(GetAllCompetitionLevels)}.{statusCode}";
    }
}
