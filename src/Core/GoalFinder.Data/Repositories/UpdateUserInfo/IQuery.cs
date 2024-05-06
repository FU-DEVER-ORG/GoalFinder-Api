using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.UpdateUserInfo;

/// <summary>
///     Repository interface for the update user info feature.
/// </summary>

public partial interface IUpdateUserInfoRepository
{
    Task<bool> IsNickNameAlreadyTakenQueryAsync(
        Guid currentUserId,
        string nickName,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );

    Task<UserDetail> GetUserDetailsQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsExperienceFoundByIdQueryAsync(
        Guid experienceId,
        CancellationToken cancellationToken
    );

    Task<bool> IsCompetitionLevelFoundByIdQueryAsync(
        Guid competitionLevelId,
        CancellationToken cancellationToken
    );

    Task<bool> ArePositionsFoundByIdsQueryAsync(
        IEnumerable<Guid> positionIds,
        CancellationToken cancellationToken
    );
}
