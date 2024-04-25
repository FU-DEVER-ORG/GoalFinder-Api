using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo;

internal partial class UpdateUserInfoRepository
{
    public Task<bool> IsUserNameAlreadyTakenQueryAsync(
        Guid currentUserId,
        string userName,
        CancellationToken cancellationToken)
    {
        return _users.AnyAsync(
            predicate: user =>
                user.Id != currentUserId &&
                user.UserName.Equals(userName),
            cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserFoundByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _userDetails.AnyAsync(
            predicate: userDetail => userDetail.UserId == userId,
            cancellationToken: cancellationToken);
    }

    public Task<UserDetail> GetUserDetailsQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _userDetails
            .AsNoTracking()
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(selector: userDetail => new UserDetail
            {
                UserId = userDetail.UserId,
                User = new()
                {
                    UserName = userDetail.User.UserName,
                    ConcurrencyStamp = userDetail.User.ConcurrencyStamp
                },
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                Description = userDetail.Description,
                AvatarUrl = userDetail.AvatarUrl,
                Address = userDetail.Address,
                ExperienceId = userDetail.ExperienceId,
                CompetitionLevelId = userDetail.CompetitionLevelId,
                UserPositions = userDetail.UserPositions.Select(
                    userPosition => new UserPosition
                    {
                        PositionId = userPosition.PositionId
                    }),
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsCompetitionLevelFoundByIdQueryAsync(
        Guid competitionLevelId,
        CancellationToken cancellationToken)
    {
        return _competitionLevels.AnyAsync(
            competitionLevel => competitionLevel.Id
                .Equals(competitionLevelId),
            cancellationToken: cancellationToken);
    }

    public Task<bool> IsExperienceFoundByIdQueryAsync(
        Guid experienceId,
        CancellationToken cancellationToken)
    {
        return _experiences.AnyAsync(
            experience => experience.Id
                .Equals(experienceId),
            cancellationToken: cancellationToken);
    }

    public async Task<bool> ArePositionsFoundByIdsQueryAsync(
        IEnumerable<Guid> positionIds,
        CancellationToken cancellationToken)
    {
        var foundPositionCount = await _positions
            .Where(predicate: position => positionIds
                .Contains(position.Id))
            .CountAsync(cancellationToken: cancellationToken);

        return foundPositionCount == positionIds.Count();
    }
}
