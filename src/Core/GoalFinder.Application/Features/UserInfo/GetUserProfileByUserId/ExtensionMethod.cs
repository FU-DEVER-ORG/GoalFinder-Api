namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

public static class ExtensionMethod
{
    public static string ToAppCode(this GetUserProfileByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(UserInfo)}.{nameof(GetUserProfileByUserId)}.{statusCode}";
    }
}
