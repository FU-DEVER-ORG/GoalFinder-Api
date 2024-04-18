using GoalFinder.Data.Entities.Base;
using System;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "RefreshTokens" table.
/// </summary>
public class RefreshToken : IBaseEntity
{
    // Primary keys.
    public Guid AccessTokenId { get; set; }

    // Normal columns.
    public string RefreshTokenValue { get; set; }

    public DateTime ExpiredDate { get; set; }

    public DateTime CreatedAt { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class RefreshTokenValue
        {
            public const int MinLength = 2;
        }
    }
}