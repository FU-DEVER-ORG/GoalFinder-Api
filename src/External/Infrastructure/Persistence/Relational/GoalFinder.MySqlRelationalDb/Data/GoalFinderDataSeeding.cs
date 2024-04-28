using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Application.Shared.FIleObjectStorage;
using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data;

public static class GoalFinderDataSeeding
{
    private static readonly Guid AdminId = Guid.Parse(
        input: "1a6c3e77-4097-40e2-b447-f00d1f82cf78"
    );

    /// <summary>
    ///     Seed data.
    /// </summary>
    /// <param name="context">
    ///     Database context for interacting with other table.
    /// </param>
    /// <param name="userManager">
    ///     Specific manager for interacting with user table.
    /// </param>
    /// <param name="roleManager">
    ///     Specific manager for interacting with role table.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if seeding is success. Otherwise, false.
    /// </returns>
    public static async Task<bool> SeedAsync(
        GoalFinderContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler,
        CancellationToken cancellationToken
    )
    {
        var experiences = context.Set<Experience>();
        var competitionLevels = context.Set<CompetitionLevel>();
        var positions = context.Set<Position>();
        var userDetails = context.Set<UserDetail>();
        var roles = context.Set<Role>();

        // Is departments table empty.
        var isTableEmpty = await IsTableEmptyAsync(
            experiences: experiences,
            competitionLevels: competitionLevels,
            positions: positions,
            userDetails: userDetails,
            roles: roles,
            cancellationToken: cancellationToken
        );

        if (!isTableEmpty)
        {
            return true;
        }

        // Init list of new experiences.
        var newExperiences = InitNewExperience();

        // Init list of new competition levels.
        var newCompetitionLevels = InitNewCompetitionLevels();

        // Init list of role.
        var newRoles = InitNewRoles();

        // Init list of position.
        var newPositions = InitNewPositions();

        //// Init admin.
        var admin = InitAdmin(
            competitionLevelId: newCompetitionLevels
                .Find(match: competitionLevel => competitionLevel.FullName.Equals(value: "Vui vẻ"))
                .Id,
            experienceId: newExperiences
                .Find(match: experience => experience.FullName.Equals(value: "Chuyên nghiệp"))
                .Id,
            defaultUserAvatarAsUrlHandler: defaultUserAvatarAsUrlHandler
        );

        #region Transaction
        var executedTransactionResult = false;

        await context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await experiences.AddRangeAsync(
                        entities: newExperiences,
                        cancellationToken: cancellationToken
                    );

                    await competitionLevels.AddRangeAsync(
                        entities: newCompetitionLevels,
                        cancellationToken: cancellationToken
                    );

                    await positions.AddRangeAsync(
                        entities: newPositions,
                        cancellationToken: cancellationToken
                    );

                    foreach (var newRole in newRoles)
                    {
                        await roleManager.CreateAsync(role: newRole);
                    }

                    await userManager.CreateAsync(user: admin, password: "Admin123@");

                    await userManager.AddToRoleAsync(user: admin, role: "admin");

                    var emailConfirmationToken =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: admin);

                    await userManager.ConfirmEmailAsync(user: admin, token: emailConfirmationToken);

                    await context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        #endregion

        return executedTransactionResult;
    }

    /// <summary>
    ///    Are tables of database empty.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if table is empty. Otherwise, false.
    /// </returns>
    private static async Task<bool> IsTableEmptyAsync(
        DbSet<Experience> experiences,
        DbSet<CompetitionLevel> competitionLevels,
        DbSet<Position> positions,
        DbSet<UserDetail> userDetails,
        DbSet<Role> roles,
        CancellationToken cancellationToken
    )
    {
        // Is experiences table empty.
        var isTableNotEmpty = await experiences.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is competition levels table empty.
        isTableNotEmpty = await competitionLevels.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is positions table empty.
        isTableNotEmpty = await positions.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is user details table empty.
        isTableNotEmpty = await userDetails.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // Is roles table empty.
        isTableNotEmpty = await roles.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        return !isTableNotEmpty;
    }

    /// <summary>
    ///     Init a list of new experience.
    /// </summary>
    /// <returns>
    ///     List of experience.
    /// </returns>
    private static List<Experience> InitNewExperience()
    {
        Dictionary<Guid, string> newExperienceNames = [];

        newExperienceNames.Add(
            key: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            value: string.Empty
        );

        newExperienceNames.Add(
            key: Guid.Parse(input: "8e9bd942-4472-4c19-bdd4-8bab0d6346e2"),
            value: "Chuyên nghiệp"
        );

        newExperienceNames.Add(
            key: Guid.Parse(input: "c99b2f00-cf5a-468f-a0ae-31cd95fecce6"),
            value: "Nghiệp dư"
        );

        List<Experience> newExperiences = [];

        foreach (var newExperienceName in newExperienceNames)
        {
            newExperiences.Add(
                item: new()
                {
                    Id = newExperienceName.Key,
                    FullName = newExperienceName.Value,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = AdminId,
                    UpdatedAt = DateTime.MinValue,
                    UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                    RemovedAt = DateTime.MinValue,
                    RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                }
            );
        }

        return newExperiences;
    }

    /// <summary>
    ///     Init a list of new competition level.
    /// </summary>
    /// <returns>
    ///     List of competition level.
    /// </returns>
    private static List<CompetitionLevel> InitNewCompetitionLevels()
    {
        Dictionary<Guid, string> newCompetitionLevelNames = [];

        newCompetitionLevelNames.Add(
            key: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            value: string.Empty
        );

        newCompetitionLevelNames.Add(
            key: Guid.Parse(input: "02569b52-d331-4b39-a89b-737cc0c55b13"),
            value: "Vui vẻ"
        );

        newCompetitionLevelNames.Add(
            key: Guid.Parse(input: "0a0a9174-e2ab-49ca-943f-dc62c26eb032"),
            value: "Vừa phải"
        );

        newCompetitionLevelNames.Add(
            key: Guid.Parse("67c22803-9fef-45e4-9f93-184db1a15458"),
            value: "Nghiêm túc"
        );

        List<CompetitionLevel> newCompetitionLevels = [];

        foreach (var newCompetitionLevelName in newCompetitionLevelNames)
        {
            newCompetitionLevels.Add(
                new()
                {
                    Id = newCompetitionLevelName.Key,
                    FullName = newCompetitionLevelName.Value,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = AdminId,
                    UpdatedAt = DateTime.MinValue,
                    UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                    RemovedAt = DateTime.MinValue,
                    RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                }
            );
        }

        return newCompetitionLevels;
    }

    /// <summary>
    ///     Init a list of new role.
    /// </summary>
    /// <returns>
    ///     List of role.
    /// </returns>
    private static List<Role> InitNewRoles()
    {
        Dictionary<Guid, string> newRoleNames = [];

        newRoleNames.Add(
            key: Guid.Parse(input: "c39aa1ac-8ded-46be-870c-115b200b09fc"),
            value: "user"
        );

        newRoleNames.Add(
            key: Guid.Parse(input: "c8500b46-b134-4b60-85b7-8e6af1187e0c"),
            value: "admin"
        );

        List<Role> newRoles = [];

        foreach (var newRoleName in newRoleNames)
        {
            Role newRole =
                new()
                {
                    Id = newRoleName.Key,
                    Name = newRoleName.Value,
                    RoleDetail = new()
                    {
                        RoleId = newRoleName.Key,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = AdminId,
                        UpdatedAt = DateTime.MinValue,
                        UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                        RemovedAt = DateTime.MinValue,
                        RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                    }
                };

            newRoles.Add(item: newRole);
        }

        return newRoles;
    }

    /// <summary>
    ///     Init a list of positions.
    /// </summary>
    /// <returns>
    ///     List of positions.
    /// </returns>
    private static List<Position> InitNewPositions()
    {
        Dictionary<Guid, string> newPositionNames = [];

        newPositionNames.Add(
            key: Guid.Parse(input: "126aad71-81e0-4e56-8d74-c1d3f3e9b8c0"),
            value: "TIền đạo"
        );

        newPositionNames.Add(
            key: Guid.Parse(input: "7bfadb87-4950-4627-aa93-c0312ff492a5"),
            value: "Hậu vệ"
        );

        newPositionNames.Add(
            key: Guid.Parse(input: "1e057224-2d18-459d-af0d-146c4c7d3a65"),
            value: "Tiền vệ"
        );

        newPositionNames.Add(
            key: Guid.Parse(input: "697ed101-07cb-4745-a80f-488e695c830a"),
            value: "Hậu vệ"
        );

        List<Position> newPositions = [];

        foreach (var newPositionName in newPositionNames)
        {
            newPositions.Add(
                new()
                {
                    Id = newPositionName.Key,
                    FullName = newPositionName.Value,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = AdminId,
                    UpdatedAt = DateTime.MinValue,
                    UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                    RemovedAt = DateTime.MinValue,
                    RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                }
            );
        }

        return newPositions;
    }

    /// <summary>
    ///     Init a list of admin.
    /// </summary>
    /// <param name="userJoiningStatus">
    ///     User joining status for admin.
    /// </param>
    /// <returns>
    ///     List of role.
    /// </returns>
    private static User InitAdmin(
        Guid competitionLevelId,
        Guid experienceId,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler
    )
    {
        User admin =
            new()
            {
                Id = AdminId,
                UserName = "ledinhdangkhoa10a9@gmail.com",
                Email = "ledinhdangkhoa10a9@gmail.com",
                UserDetail = new()
                {
                    UserId = AdminId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = AdminId,
                    RemovedAt = DateTime.MinValue,
                    RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                    UpdatedAt = DateTime.MinValue,
                    UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                    Address = "Thanh pho da nang - Quan son tra",
                    CompetitionLevelId = competitionLevelId,
                    FirstName = "Khoa",
                    LastName = "Le",
                    Description = "Hi my name is khoa, admin of this website.",
                    ExperienceId = experienceId,
                    PrestigeScore = 100,
                    BackgroundUrl = string.Empty,
                    AvatarUrl = defaultUserAvatarAsUrlHandler.Get()
                }
            };

        return admin;
    }
}
