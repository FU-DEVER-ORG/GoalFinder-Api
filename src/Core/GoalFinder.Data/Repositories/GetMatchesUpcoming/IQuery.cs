using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetMatchesUpcoming;

public partial interface IGetMatchesUpcomingRepository
{
    Task<IEnumerable<FootballMatch>> GetMatchesUpComingByUserIdQuery(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserFoundByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
