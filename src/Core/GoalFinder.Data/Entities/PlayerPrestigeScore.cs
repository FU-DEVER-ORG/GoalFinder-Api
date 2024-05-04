using System;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

public class PlayerPrestigeScore
{
    public Guid PlayerId { get; set; }
    public int bonusScore { get; set; }

    public PlayerPrestigeScore(Guid playerId, int prestigeScore)
    {
        PlayerId = playerId;
        bonusScore = prestigeScore;
    }
}
