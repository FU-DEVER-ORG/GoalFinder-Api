using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
/// Report user after match request
/// </summary>
public sealed class ReportUserAfterMatchRequest : IFeatureRequest<ReportUserAfterMatchResponse>
{
    public Guid FootballMatchId { get; init; }

    public Guid UserId { get; init; }

    public List<PlayerPrestigeScore> PlayerScores { get; set; } = new List<PlayerPrestigeScore>();

    public DateTime CurrentTime { get; init; }

}