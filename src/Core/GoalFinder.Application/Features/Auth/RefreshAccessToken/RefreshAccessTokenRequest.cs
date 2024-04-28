using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

public class RefreshAccessTokenRequest : IFeatureRequest<RefreshAccessTokenResponse>
{
    public string RefreshToken { get; set; }
}
