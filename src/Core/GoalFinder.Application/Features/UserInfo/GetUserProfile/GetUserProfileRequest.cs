using FastEndpoints;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;

/// <summary>
///     Get User Profile Request
/// </summary>
public sealed class GetUserProfileRequest : IFeatureRequest<GetUserProfileResponse>
{
    [BindFrom("nickname")]
    public string NickName { get; init; }
}
