using System;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "MatchPlayers" table.
/// </summary>
public class MatchPlayer : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid MatchId { get; set; }

    public Guid PlayerId { get; set; }

    public Guid JoiningStatusId { get; set; }

    public int NumberOfReports { get; set; }

    public bool IsReported { get; set; }

    // Navigation properties.
    public FootballMatch FootballMatch { get; set; }

    public UserDetail UserDetail { get; set; }

    public MatchPlayerJoiningStatus MatchPlayerJoiningStatus { get; set; }
}
