using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ReportUserAfterMatch;

public partial interface IReportUserAfterMatchRepository
{
    Task<bool> IsUserFoundByIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<bool> IsFootballMatchFoundByIdQueryAsync(
        Guid footballMatchId,
        CancellationToken cancellationToken
    );

    Task<bool> IsPlayerExistInFootballMatchQueryAsync(
        Guid footballMatchId,
        Guid userId,
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

    Task<bool> IsFormWithin24HoursQueryAsync(
        Guid footballMatchId,
        DateTime currentTime,
        CancellationToken cancellationToken
    );
}
