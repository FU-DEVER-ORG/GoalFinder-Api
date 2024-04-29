namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///  ExtensionMethod for RefreshAccessToken
/// </summary>

public static class ExtensionMethod
{
    /// <summary>
    /// Convert RefreshAccessTokenResponseStatusCode to AppCode
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>

    public static string ToAppCode(this RefreshAccessTokenResponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(RefreshAccessToken)}.{statusCode}";
    }
}
