namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Extension method for update user info feature.
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
    public static string ToAppCode(this UpdateUserInfoResponseStatusCode statusCode)
    {
        return $"{nameof(User)}.{nameof(UpdateUserInfo)}.{statusCode}";
    }
}
