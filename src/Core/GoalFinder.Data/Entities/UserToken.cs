using System;
using GoalFinder.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "UserTokens" table.
/// </summary>
public class UserToken : IdentityUserToken<Guid>, IBaseEntity
{
    public DateTime ExpiredAt { get; set; }

    // Navigation properties.
    public User User { get; set; }
}
