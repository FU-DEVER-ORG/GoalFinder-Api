using GoalFinder.Application.Shared.Features;
using System;
using System.Collections.Generic;

namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Update user info request.
/// </summary>
public sealed class UpdateUserInfoRequest : IFeatureRequest<UpdateUserInfoResponse>
{
    private Guid _userId;

    public string UserName { get; init; }

    public string LastName { get; init; }

    public string FirstName { get; init; }

    public string Description { get; init; }

    public string Address { get; init; }

    public string AvatarUrl { get; init; }

    public Guid ExperienceId { get; init; }

    public IEnumerable<Guid> PositionIds { get; set; }

    public Guid CompetitionLevelId { get; init; }

    public void SetUserId(Guid userId)
    {
        _userId = userId;
    }

    public Guid GetUserId()
    {
        return _userId;
    }
}
