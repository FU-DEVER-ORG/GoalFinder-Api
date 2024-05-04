using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Response for RefreshAccessToken
/// </summary>
public sealed class RefreshAccessTokenResponse : IFeatureResponse
{
    public RefreshAccessTokenResponseStatusCode StatusCode { get; set; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string AccessToken { get; init; }

        public string RefreshToken { get; init; }
    }
}
