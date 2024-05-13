using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.CreateMatch;

/// <summary>
///     Interface for CreateMatchRepository
/// </summary>
public partial interface ICreateMatchRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<bool> IsCompetitionLevelFoundByIdQueryAsync(
        Guid competitionLevelId,
        CancellationToken cancellationToken
    );

    Task<bool> HasUserCreatedMatchThisDayQueryAsync(
        Guid userId,
        DateTime startTime,
        CancellationToken cancellationToken
    );

    public Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
}
