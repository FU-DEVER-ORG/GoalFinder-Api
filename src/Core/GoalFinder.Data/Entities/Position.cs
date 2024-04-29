using System;
using System.Collections.Generic;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "Positions" table.
/// </summary>
public class Position : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string FullName { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserPosition> UserPositions { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class FullName
        {
            public const int MinLength = 3;

            public const int MaxLength = 50;
        }
    }
}
