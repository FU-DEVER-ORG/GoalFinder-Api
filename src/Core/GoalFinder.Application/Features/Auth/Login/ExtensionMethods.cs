namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Extension method for login feature.
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
    public static string ToAppCode(this LoginResponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(Login)}.{statusCode}";
    }
}
