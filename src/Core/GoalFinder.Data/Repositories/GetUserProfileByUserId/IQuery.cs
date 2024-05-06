using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetUserProfileByUserId;

/// <summary>
/// Interface for <see cref="IGetUserProfileByUserIdRepository"/>
/// </summary>
public partial interface IGetUserProfileByUserIdRepository
{
    Task<User> GetUserByIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<FootballMatch>> GetFootballMatchByIdAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
