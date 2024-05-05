using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ReportUserAfterMatch;

internal partial class ReportUserAfterMatchRepository
{
    public Task<bool> IsUserFoundByIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _userDetails.AnyAsync(
            predicate: userDetail => userDetail.UserId == userId,
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

    public Task<bool> IsFootballMatchFoundByIdQueryAsync(
        Guid footballMatchId,
        CancellationToken cancellationToken
    )
    {
        return _footballMatch.AnyAsync(
            predicate: footballMatch => footballMatch.Id.Equals(footballMatchId),
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsPlayerExistInFootballMatchQueryAsync(
        Guid footballMatchId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _matchPlayer.AnyAsync(
            predicate: matchPlayer =>
                matchPlayer.MatchId == footballMatchId && matchPlayer.UserDetail.UserId == userId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsEndTimeFootballMatchDoneQueryAsync(
        Guid footballMatchId,
        DateTime currentTime,
        CancellationToken cancellationToken
    )
    {
        return _footballMatch.AnyAsync(
            predicate: footballMatch => footballMatch.EndTime >= currentTime,
            cancellationToken: cancellationToken
        );
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

    public Task<bool> IsFormWithin24HoursQueryAsync(
        Guid footballMatchId,
        DateTime currentTime,
        CancellationToken cancellationToken
    )
    {
        return _footballMatch.AnyAsync(
                predicate: footballMatch => footballMatch.Id == footballMatchId &&
                                            footballMatch.EndTime <= currentTime &&
                                            footballMatch.EndTime > currentTime.AddDays(-1),
                cancellationToken: cancellationToken
            );
    }

    public Task<UserDetail> GetUserDetailByIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _userDetails
           .AsNoTracking()
           .Where(userDetail => userDetail.UserId == userId)
           .Select(userDetail => new UserDetail
           {
               User = new() { UserName = userDetail.User.UserName },
               PrestigeScore = userDetail.PrestigeScore
           })
           .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
