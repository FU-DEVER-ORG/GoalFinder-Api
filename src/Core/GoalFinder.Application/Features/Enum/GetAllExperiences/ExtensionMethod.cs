namespace GoalFinder.Application.Features.Enum.GetAllExperiences;

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
    public static string ToAppCode(this GetAllExperiencesResponseStatusCode statusCode)
    {
        return $"{nameof(Enum)}.{nameof(GetAllExperiences)}.{statusCode}";
    }
}
