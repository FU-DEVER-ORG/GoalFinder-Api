using System;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "RefreshTokens" table.
/// </summary>
public class RefreshToken : IBaseEntity
{
    // Primary keys.
    public Guid UserId { get; set; }

    public Guid AccessTokenId { get; set; }

    // Normal columns.
    public string RefreshTokenValue { get; set; }

    public DateTime ExpiredDate { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation properties.
    public UserDetail UserDetail { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class RefreshTokenValue
        {
            public const int MinLength = 2;
        }
    }
}
