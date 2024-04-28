using System;
using System.Collections.Generic;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "FootballMatches" table.
/// </summary>
public class FootballMatch : IBaseEntity, ICreatedEntity, IUpdatedEntity, ITemporarilyRemovedEntity
{
    // Primary keys.
    public Guid Id { get; set; }

    // Normal columns.
    public string PitchAddress { get; set; }

    public int MaxMatchPlayersNeed { get; set; }

    public decimal PitchPrice { get; set; }

    public string Description { get; set; }

    public int MinPrestigeScore { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    public string Address { get; set; }

    // Foreign keys.
    public Guid HostId { get; set; }

    public Guid CompetitionLevelId { get; set; }

    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    public CompetitionLevel CompetitionLevel { get; set; }

    // Navigation collections.
    public IEnumerable<MatchPlayer> MatchPlayers { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class PitchAddress
        {
            public const int MinLength = 3;

            public const int MaxLength = 100;
        }

        public static class Description
        {
            public const int MinLength = 3;
        }

        public static class MaxMatchPlayersNeed
        {
            public const int MinValue = 1;
        }

        public static class PitchPrice
        {
            public const int MinValue = 1000;
        }

        public static class MinPrestigeScore
        {
            public const int MinValue = 1;

            public const int MaxValue = 100;
        }

        public static class Address
        {
            public const int MinLength = 1;

            public const int MaxLength = 200;
        }
    }
}
