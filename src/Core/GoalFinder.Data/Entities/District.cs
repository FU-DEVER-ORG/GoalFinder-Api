using GoalFinder.Data.Entities.Base;
using System;
using System.Collections.Generic;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "Districts" table.
/// </summary>
public class District :
    IBaseEntity,
    ICreatedEntity,
    IUpdatedEntity,
    ITemporarilyRemovedEntity
{
    internal District() { }

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

    // Foreign keys.
    public Guid ProvinceId { get; set; }

    // Navigation properties.
    public Province Province { get; set; }

    // Navigation collections.
    public IEnumerable<Ward> Wards { get; set; }

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
