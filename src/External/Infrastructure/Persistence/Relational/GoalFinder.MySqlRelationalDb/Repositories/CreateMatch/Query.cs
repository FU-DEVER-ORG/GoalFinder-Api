using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.CreateMatch;

/// <summary>
///     Implementation of <see cref="ICreateMatchQuery"/>
/// </summary>
internal sealed partial class CreateMatchRepository
{
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

    public Task<bool> IsUserCreatedMatchThisDayQueryAsync(
        Guid userId,
        DateTime startTime,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .Where(userDetail => userDetail.UserId == userId)
            .SelectMany(userDetail => userDetail.FootballMatches)
            .AnyAsync(
                footballMatch => footballMatch.StartTime.Date.Equals(startTime.Date),
                cancellationToken
            );
    }

    public Task<UserDetail> GetUserDetailByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(userDetail => new UserDetail { PrestigeScore = userDetail.PrestigeScore, })
            .FirstAsync(cancellationToken: cancellationToken);
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
