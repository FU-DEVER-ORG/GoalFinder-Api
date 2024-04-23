namespace GoalFinder.Application.Features.Auth.Register;

/// <summary>
///     Extension method for register as user feature.
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
    public static string ToAppCode(this RegisterAsUserResponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(Register)}.{statusCode}";
    }
}
