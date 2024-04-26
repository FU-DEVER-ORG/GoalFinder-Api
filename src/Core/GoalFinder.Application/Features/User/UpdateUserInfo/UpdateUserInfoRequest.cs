using GoalFinder.Application.Shared.Features;
using System;
using System.Collections.Generic;

namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Update user info request.
/// </summary>
public sealed class UpdateUserInfoRequest : IFeatureRequest<UpdateUserInfoResponse>
{
    public Guid UserId { get; set; }

    public string UserName { get; init; }

    public string LastName { get; init; }

    public string FirstName { get; init; }

    public string Description { get; init; }

    public string Address { get; init; }

    public string AvatarUrl { get; init; }

    public Guid ExperienceId { get; init; }

    public IEnumerable<Guid> PositionIds { get; set; }

    public Guid CompetitionLevelId { get; init; }
}
