namespace GoalFinder.Application.Features.User.GetDropdownAvatar;

/// <summary>
///     Extension Method for get user info on dropdown avatar features.
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
    public static string ToAppCode(this GetDropdownAvatarResponseStatusCode statusCode)
    {
        return $"{nameof(User)}.{nameof(GetUserInfoOnSidebar)}.{statusCode}";
    }
}
