namespace GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

/// <summary>
///     Extension Method for get user info on sidebar features.
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
    public static string ToAppCode(this GetUserInfoOnSidebarResponseStatusCode statusCode)
    {
        return $"{nameof(User)}.{nameof(GetUserInfoOnSidebar)}.{statusCode}";
    }
}
