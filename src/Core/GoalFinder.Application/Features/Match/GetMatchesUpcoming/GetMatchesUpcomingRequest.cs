using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.GetMatchesUpcoming;

/// <summary>
///     Request for get matches upcoming features.
/// </summary>
public sealed class GetMatchesUpcomingRequest : IFeatureRequest<GetMatchesUpcomingResponse>
{
    private Guid _userId;

    public Guid GetUserId() => _userId;

    public void SetUserId(Guid userId) => _userId = userId;
}
