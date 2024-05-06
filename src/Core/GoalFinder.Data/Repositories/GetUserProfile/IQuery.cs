using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetUserProfile;

/// <summary>
///     Interface for Get User Profile Repository.
/// </summary>
public partial interface IGetUserProfileRepository
{
    Task<UserDetail> GetUserByNickNameQueryAsync(
        string nickName,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<FootballMatch>> GetFootballMatchByIdAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
