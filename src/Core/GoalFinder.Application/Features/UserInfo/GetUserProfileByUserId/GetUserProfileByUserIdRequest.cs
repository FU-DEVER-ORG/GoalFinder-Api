using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

/// <summary>
/// Get User Profile By User Id
/// </summary>
public sealed class GetUserProfileByUserIdRequest : IFeatureRequest<GetUserProfileByUserIdResponse>
{
    public Guid Id { get; set; }
}
