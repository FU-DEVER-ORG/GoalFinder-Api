using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

/// <summary>
///     Request for get user info on sidebar features.
/// </summary>
public sealed class GetUserInfoOnSidebarRequest : IFeatureRequest<GetUserInfoOnSidebarResponse>
{
    private Guid _userId;

    public Guid GetUserId() => _userId;

    public void SetUserId(Guid userId) => _userId = userId;
}
