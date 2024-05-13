using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetMatchesUpcoming;

internal partial class GetMatchesUpcomingRepository
{
    public async Task<IEnumerable<FootballMatch>> GetMatchesUpComingByUserIdQuery(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _userDetails
            .AsNoTracking()
            .Where(userDetail => userDetail.UserId == userId)
            .SelectMany(userDetail => userDetail.MatchPlayers)
            .Where(matchPlayer => matchPlayer.FootballMatch.StartTime > DateTime.UtcNow)
            .Select(matchPlayer => new FootballMatch
            {
                Description = matchPlayer.FootballMatch.Description,
                StartTime = matchPlayer.FootballMatch.StartTime,
                MaxMatchPlayersNeed = matchPlayer.FootballMatch.MaxMatchPlayersNeed,
                MatchPlayers = matchPlayer.FootballMatch.MatchPlayers.ToList(),
            })
            .ToListAsync(cancellationToken: cancellationToken);
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

    public Task<bool> IsUserFoundByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
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
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue,
            cancellationToken: cancellationToken
        );
    }
}
