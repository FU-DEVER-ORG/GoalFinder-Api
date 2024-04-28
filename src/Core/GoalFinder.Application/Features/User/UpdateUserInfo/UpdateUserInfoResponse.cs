using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Update user info response.
/// </summary>
public sealed class UpdateUserInfoResponse : IFeatureResponse
{
    public UpdateUserInfoResponseStatusCode StatusCode { get; init; }
}
