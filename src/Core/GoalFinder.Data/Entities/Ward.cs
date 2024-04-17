using GoalFinder.Data.Entities.Base;
using System;
using System.Collections.Generic;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "Wards" table.
/// </summary>
public class Ward :
    IBaseEntity,
    ICreatedEntity,
    IUpdatedEntity,
    ITemporarilyRemovedEntity
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

    // Foreign keys.
    public Guid DistrictId { get; set; }

    // Navigation properties.
    public District District { get; set; }

    // Navigation collections.
    public IEnumerable<UserDetail> UserDetails { get; set; }

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
