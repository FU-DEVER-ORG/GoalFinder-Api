using GoalFinder.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "UserTokens" table.
/// </summary>
public class UserToken :
    IdentityUserToken<Guid>,
    IBaseEntity
{
    internal UserToken() { }

    // Navigation properties.
    public User User { get; set; }
}