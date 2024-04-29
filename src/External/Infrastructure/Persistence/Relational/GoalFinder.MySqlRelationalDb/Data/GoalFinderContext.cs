using System;
using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data;

public sealed class GoalFinderContext : IdentityDbContext<User, Role, Guid>
{
    public GoalFinderContext(DbContextOptions<GoalFinderContext> options)
        : base(options: options) { }

    /// <summary>
    ///     Configure tables and seed initial data here.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder: builder);

        builder.ApplyConfigurationsFromAssembly(typeof(Commons.CommonConstant).Assembly);

        RemoveAspNetPrefixInIdentityTable(builder: builder);
    }

    /// <summary>
    ///     Remove "AspNet" prefix in identity table name.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
    /// </param>
    private static void RemoveAspNetPrefixInIdentityTable(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();

            if (tableName.StartsWith(value: "AspNet"))
            {
                entityType.SetTableName(name: tableName[6..]);
            }
        }
    }
}
