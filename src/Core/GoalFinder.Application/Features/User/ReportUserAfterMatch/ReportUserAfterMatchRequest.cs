using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
/// Report user after match request
/// </summary>
public sealed class ReportUserAfterMatchRequest : IFeatureRequest<ReportUserAfterMatchResponse>
{
    public Guid _footballMatchId;

    public Guid _playerId;

    public int PrestigeScore;

    public DateTime currentTime;

    public void SetFootballMatchId(Guid footballMatchId)
    {
        _footballMatchId = footballMatchId;
    }

    public Guid GetFootballMatchId()
    {
        return _footballMatchId;
    }

    public void SetPlayerId(Guid playerId)
    {
        _playerId = playerId;
    }

    public Guid GetPlayerId()
    {
        return _playerId;
    }
}
