using GoalFinder.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class Role :
    IdentityRole<Guid>,
    IBaseEntity
{
    internal Role() { }

    // Navigation properties.
    public RoleDetail RoleDetail { get; set; }

    // Navigation collections.
    public IEnumerable<UserRole> UserRoles { get; set; }

    // Additional information of this table.
    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 50;

            public const int MinLength = 2;
        }
    }
}