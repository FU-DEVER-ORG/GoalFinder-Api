using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.ReportUserAfterMatch;

public partial interface IReportUserAfterMatchRepository
{
    Task<bool> IsUserFoundByIdQueryAsync(Guid playerId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid playerId,
        CancellationToken cancellationToken
    );

    Task<bool> IsFootballMatchFoundByIdQueryAsync(
        Guid footballMatchId,
        CancellationToken cancellationToken
    );

    Task<bool> IsPlayerExistInFootballMatchQueryAsync(
        Guid footballMatchId,
        Guid playerId,
        CancellationToken cancellationToken
    );

    Task<bool> IsEndTimeFootballMatchDoneQueryAsync(
        Guid footballMatchId,
        DateTime currentTime,
        CancellationToken cancellationToken
    );

    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
}
