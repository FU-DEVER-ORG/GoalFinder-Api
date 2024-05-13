namespace GoalFinder.Application.Features.Enum.GetAllPositions;

/// <summary>
///     Extension Method for get all positions features.
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
    public static string ToAppCode(this GetAllPositionsResponseStatusCode statusCode)
    {
        return $"{nameof(Enum)}.{nameof(GetAllPositions)}.{statusCode}";
    }
}
