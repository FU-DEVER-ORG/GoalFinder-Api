using System;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "UserPositions" table.
/// </summary>
public class UserPosition : IBaseEntity
{
    // Primary keys.
    // Foreign keys.
    public Guid UserId { get; set; }

    public Guid PositionId { get; set; }

    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    public Position Position { get; set; }
}
