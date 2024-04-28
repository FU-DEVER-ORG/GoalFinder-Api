namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///  ExtensionMethod for RefreshAccessToken
/// </summary>

public static class ExtensionMethod
{
    public static string ToAppCode (this RefreshAccessTokenResponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(RefreshAccessToken)}.{statusCode}";
    }
}
