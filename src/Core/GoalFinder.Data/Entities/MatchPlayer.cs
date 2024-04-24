using GoalFinder.Data.Entities.Base;
using System;

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
    
    public int NumberOfReports { get; set; }

    // Navigation properties.
    public FootballMatch FootballMatch { get; set; }

    public UserDetail UserDetail { get; set; }
}
