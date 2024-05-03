using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.GetDropdownAvatar;

/// <summary>
///     Request for get user info on get dropdown avatar features.
/// </summary>
public sealed class GetDropdownAvatarRequest : IFeatureRequest<GetDropdownAvatarResponse>
{
    private Guid _userId;

    public Guid GetUserId() => _userId;

    public void SetUserId(Guid userId) => _userId = userId;
}
