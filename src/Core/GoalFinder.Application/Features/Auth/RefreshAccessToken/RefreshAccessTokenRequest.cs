using GoalFinder.Application.Shared.Features;

/// <summary>
///     Request for RefreshAccessToken
/// </summary>

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

public sealed class RefreshAccessTokenRequest : IFeatureRequest<RefreshAccessTokenResponse>
{
    public string RefreshToken { get; set; }
}
