using GoalFinder.Data.Entities.Base;
using System;
using System.Collections.Generic;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "RoleDetails" table.
/// </summary>
public class RoleDetail :
    IBaseEntity,
    ICreatedEntity,
    IUpdatedEntity,
    ITemporarilyRemovedEntity
{
    internal RoleDetail() { }

    // Primary keys.
    // Foreign keys.
    public Guid RoleId { get; set; }

    // Normal columns.
    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation properties.
    public Role Role { get; set; }

    // Navigation collections.
    public IEnumerable<UserPosition> UserPositions { get; set; }
}
