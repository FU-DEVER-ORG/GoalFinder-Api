using System;
using System.Collections.Generic;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "UserDetails" table.
/// </summary>
public class UserDetail : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid UserId { get; set; }

    // Normal columns.
    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string Description { get; set; }

    public int PrestigeScore { get; set; }

    public string Address { get; set; }

    public string BackgroundUrl { get; set; }

    public string AvatarUrl { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Foreign keys.
    public Guid ExperienceId { get; set; }

    public Guid CompetitionLevelId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Experience Experience { get; set; }

    public CompetitionLevel CompetitionLevel { get; set; }

    // Navigation collections.
    public IEnumerable<UserPosition> UserPositions { get; set; }

    public IEnumerable<MatchPlayer> MatchPlayers { get; set; }

    public IEnumerable<RefreshToken> RefreshTokens { get; set; }
    public IEnumerable<FootballMatch> FootballMatches { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class LastName
        {
            public const int MinLength = 2;

            public const int MaxLength = 50;
        }

        public static class FirstName
        {
            public const int MinLength = 2;

            public const int MaxLength = 50;
        }

        public static class Description
        {
            public const int MinLength = 2;
        }

        public static class PrestigeScore
        {
            public const int MinValue = 1;

            public const int MaxValue = 100;
        }

        public static class Address
        {
            public const int MinLength = 2;

            public const int MaxLength = 200;
        }

        public static class AvatarUrl
        {
            public const int MinLength = 2;
        }

        public static class BackgroundUrl
        {
            public const int MinLength = 2;
        }
    }
}
