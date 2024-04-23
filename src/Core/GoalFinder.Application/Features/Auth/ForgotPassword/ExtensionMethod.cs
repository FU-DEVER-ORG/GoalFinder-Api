namespace GoalFinder.Application.Features.Auth.ForgotPassword;

/// <summary>
///     Extension Method for forgot password features.
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
    public static string ToAppCode(this ForgotPasswordReponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(ForgotPassword)}.{statusCode}";
    }
}
