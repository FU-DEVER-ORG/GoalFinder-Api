using System;
using FastEndpoints;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

public sealed class GetUserProfileByUserIdRequest : IFeatureRequest<GetUserProfileByUserIdResponse>
{
    [BindFrom("id")]
    public Guid Id { get; set; }
}
