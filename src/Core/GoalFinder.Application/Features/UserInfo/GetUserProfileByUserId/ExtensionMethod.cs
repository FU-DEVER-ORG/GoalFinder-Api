namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

/// <summary>
/// Extension method for <see cref="GetUserProfileByUserIdResponseStatusCode"/>
/// </summary>
///
public static class ExtensionMethod
{
    public static string ToAppCode(this GetUserProfileByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(UserInfo)}.{nameof(GetUserProfileByUserId)}.{statusCode}";
    }
}
