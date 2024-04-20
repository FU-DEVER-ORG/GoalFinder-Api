using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using GoalFinder.Application.Shared.FIleObjectStorage;

namespace GoalFinder.MySqlRelationalDb.Data;

public static class GoalFinderDataSeeding
{
    private static readonly Guid AdminId = Guid.Parse("1a6c3e77-4097-40e2-b447-f00d1f82cf78");

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
        CancellationToken cancellationToken)
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
            cancellationToken: cancellationToken);

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

        //// Init admin.
        //var admin = InitAdmin(
        //    userJoiningStatus: newUserJoiningStatuses.First(
        //        predicate: newUserJoiningStatus =>
        //            newUserJoiningStatus.Type.Equals(value: "Approved")),
        //    defaultUserAvatarAsUrlHandler: defaultUserAvatarAsUrlHandler);

        #region Transaction
        var executedTransactionResult = false;

        //await context.Database
        //    .CreateExecutionStrategy()
        //    .ExecuteAsync(operation: async () =>
        //    {
        //        await using var dbTransaction = await context.Database.BeginTransactionAsync(
        //            cancellationToken: cancellationToken);

        //        try
        //        {
        //            await experiences.AddRangeAsync(
        //                entities: newExperiences,
        //                cancellationToken: cancellationToken);

        //            foreach (var newRole in newRoles)
        //            {
        //                await roleManager.CreateAsync(role: newRole);
        //            }

        //            await userManager.CreateAsync(
        //                user: admin,
        //                password: "Admin123@");

        //            await userManager.AddToRoleAsync(
        //                user: admin,
        //                role: "admin");

        //            var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user: admin);

        //            await userManager.ConfirmEmailAsync(
        //                user: admin,
        //                token: emailConfirmationToken);

        //            await context.SaveChangesAsync(cancellationToken: cancellationToken);

        //            await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

        //            executedTransactionResult = true;
        //        }
        //        catch
        //        {
        //            await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
        //        }
        //    });
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
        CancellationToken cancellationToken)
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
        List<string> newExperienceNames =
        [
            string.Empty,
            "Chuyên nghiệp",
            "Nghiệp dư",
        ];

        List<Experience> newExperiences = [];

        foreach (var newExperienceName in newExperienceNames)
        {
            newExperiences.Add(item: new()
            {
                Id = Guid.NewGuid(),
                FullName = newExperienceName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = AdminId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = DateTime.UtcNow,
                RemovedBy = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
            });
        }

        newExperiences
            .Find(match: newExperience => newExperience.FullName.Equals(value: newExperienceNames[0]))
            .Id = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID;

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
        List<string> newCompetitionLevelNames =
        [
            string.Empty,
            "Vui vẻ",
            "Vừa phải",
            "Nghiêm túc"
        ];

        List<CompetitionLevel> newCompetitionLevels = [];

        foreach (var newCompetitionLevelName in newCompetitionLevelNames)
        {
            newCompetitionLevels.Add(new()
            {
                Id = Guid.NewGuid(),
                FullName = newCompetitionLevelName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = AdminId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = DateTime.UtcNow,
                RemovedBy = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
            });
        }

        newCompetitionLevels
            .Find(match: newHobby => newHobby.FullName.Equals(value: newCompetitionLevelNames[0]))
            .Id = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID;

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
        List<string> newRoleNames =
        [
            "admin",
            "user"
        ];

        List<RoleDetail> newRoleDetails = [];
        List<Role> newRoles = [];

        foreach (var newRoleName in newRoleNames)
        {
            Role newRole = new()
            {
                Id = Guid.NewGuid(),
                Name = newRoleName
            };

            newRoles.Add(item: newRole);

            newRoleDetails.Add(new()
            {
                RoleId = newRole.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = AdminId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = DateTime.UtcNow,
                RemovedBy = Application.Others.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
            });
        }

        return newRoles;
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
    //private static User InitAdmin(
    //    UserJoiningStatus userJoiningStatus,
    //    IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler)
    //{
    //    UserForDatabaseSeedingBuilder userBuilder = new();
    //    UserDetailForDatabaseSeedingBuilder userDetailBuilder = new();

    //    var admin = userBuilder
    //        .WithId(userId: Guid.NewGuid())
    //        .WithUserName(username: "ledinhdangkhoa10a9@gmail.com")
    //        .WithEmail(userEmail: "ledinhdangkhoa10a9@gmail.com")
    //        .Complete();

    //    admin.UserDetail = userDetailBuilder
    //        .WithUserJoiningStatusId(userJoiningStatusId: userJoiningStatus.Id)
    //        .WithPositionId(userPositionId: Application.Shared.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
    //        .WithMajorId(userMajorId: Application.Shared.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
    //        .WithDepartmentId(userDepartmentId: Application.Shared.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
    //        .WithFirstName(userFirstName: string.Empty)
    //        .WithLastName(userLastName: string.Empty)
    //        .WithCareer(userCareer: string.Empty)
    //        .WithWorkplaces(userWorkplaces: string.Empty)
    //        .WithEducationPlaces(userEducationPlaces: string.Empty)
    //        .WithUserHobbies(userHobbies: string.Empty)
    //        .WithUserSkills(userSkills: string.Empty)
    //        .WithUserPlatforms(userPlatforms: string.Empty)
    //        .WithBirthDay(userBirthDay: Commons.CommonConstant.DbDefaultValue.MIN_DATE_TIME)
    //        .WithHomeAddress(userHomeAddress: string.Empty)
    //        .WithSelfDescription(userSelfDescription: string.Empty)
    //        .WithJoinDate(userJoinDate: DateTime.UtcNow)
    //        .WithAvatarUrl(userAvatarUrl: defaultUserAvatarAsUrlHandler.Get())
    //        .WithRemovedAt(userRemovedAt: Commons.CommonConstant.DbDefaultValue.MIN_DATE_TIME)
    //        .WithRemovedBy(userRemovedBy: Application.Shared.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
    //        .WithUpdatedAt(userUpdatedAt: Commons.CommonConstant.DbDefaultValue.MIN_DATE_TIME)
    //        .WithUpdatedBy(userUpdatedBy: Application.Shared.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
    //        .WithCreatedAt(userCreatedAt: DateTime.UtcNow)
    //        .WithCreatedBy(userCreatedBy: Application.Shared.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
    //        .Complete();

    //    return admin;
    //}
}
