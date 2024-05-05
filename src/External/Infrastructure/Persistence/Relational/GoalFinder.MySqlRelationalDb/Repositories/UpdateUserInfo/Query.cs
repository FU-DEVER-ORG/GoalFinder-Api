using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo;

internal partial class UpdateUserInfoRepository
{
    public Task<bool> IsNickNameAlreadyTakenQueryAsync(
        Guid currentUserId,
        string nickName,
        CancellationToken cancellationToken
    )
    {
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId != currentUserId && userDetail.NickName.Equals(nickName),
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .Where(predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue
            )
            .AnyAsync(cancellationToken: cancellationToken);
    }

    public Task<UserDetail> GetUserDetailsQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
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
                BackgroundUrl = userDetail.BackgroundUrl,
                Address = userDetail.Address,
                ExperienceId = userDetail.ExperienceId,
                CompetitionLevelId = userDetail.CompetitionLevelId,
                UserPositions = userDetail.UserPositions.Select(userPosition => new UserPosition
                {
                    PositionId = userPosition.PositionId
                }),
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsCompetitionLevelFoundByIdQueryAsync(
        Guid competitionLevelId,
        CancellationToken cancellationToken
    )
    {
        return _competitionLevels.AnyAsync(
            predicate: competitionLevel => competitionLevel.Id.Equals(competitionLevelId),
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsExperienceFoundByIdQueryAsync(
        Guid experienceId,
        CancellationToken cancellationToken
    )
    {
        return _experiences.AnyAsync(
            predicate: experience => experience.Id.Equals(experienceId),
            cancellationToken: cancellationToken
        );
    }

    public async Task<bool> ArePositionsFoundByIdsQueryAsync(
        IEnumerable<Guid> positionIds,
        CancellationToken cancellationToken
    )
    {
        var foundPositionCount = await _positions
            .Where(predicate: position => positionIds.Contains(position.Id))
            .CountAsync(cancellationToken: cancellationToken);

        return foundPositionCount == positionIds.Count();
    }

    public Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens.AnyAsync(
            predicate: refreshToken => refreshToken.AccessTokenId == accessTokenId,
            cancellationToken: cancellationToken
        );
    }
}
