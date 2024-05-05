using System;
using FastEndpoints;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.GetMatchDetail;

/// <summary>
///     Get Match Detail Request
/// </summary>

public sealed class GetMatchDetailRequest : IFeatureRequest<GetMatchDetailResponse>
{
    private Guid _userId;

    [BindFrom("matchId")]
    public Guid MatchId { get; set; }

    public void SetUserId(Guid userId)
    {
        _userId = userId;
    }

    public Guid GetUserId()
    {
        return _userId;
    }
}
