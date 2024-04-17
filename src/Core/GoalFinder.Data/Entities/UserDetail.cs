using GoalFinder.Data.Entities.Base;
using System;
using System.Collections.Generic;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "UserDetails" table.
/// </summary>
public class UserDetail : IBaseEntity
{
    internal UserDetail() { }

    // Primary keys.
    public Guid UserId { get; set; }

    // Normal columns.
    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string Description { get; set; }

    public int PrestigeScore { get; set; }

    // Foreign keys.
    public Guid WardId { get; set; }

    public Guid ExperienceId { get; set; }

    public Guid CompetitionLevelId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Ward Ward { get; set; }

    public Experience Experience { get; set; }

    public CompetitionLevel CompetitionLevel { get; set; }

    // Navigation collections.
    public IEnumerable<UserPosition> UserPositions { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class LastName
        {
            public const int MinLength = 3;

            public const int MaxLength = 50;
        }

        public static class FirstName
        {
            public const int MinLength = 3;

            public const int MaxLength = 50;
        }

        public static class Description
        {
            public const int MinLength = 3;
        }

        public static class PrestigeScore
        {
            public const int MinValue = 1;

            public const int MaxValue = 100;
        }
    }
}
