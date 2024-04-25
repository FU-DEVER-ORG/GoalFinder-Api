
namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;
/// <summary>
///     Extension Method for get user profile features.
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
    public static string ToAppCode(this GetUserProfileResponseStatusCode statusCode)
    {
        return $"{nameof(UserInfo)}.{nameof(GetUserProfile)}.{statusCode}";
    }
}
