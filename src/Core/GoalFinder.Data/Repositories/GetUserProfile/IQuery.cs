using System.Threading.Tasks;
using System.Threading;
using System;
using GoalFinder.Data.Entities;
using System.Collections.Generic;

namespace GoalFinder.Data.Repositories.GetUserProfile;

/// <summary>
///     Interface for Get User Profile Repository.
/// </summary>
public partial interface IGetUserProfileRepository
{
    Task<User> GetUserByUsernameQueryAsync(
        string userName,
        CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<IEnumerable<FootballMatch>> GetFootballMatchByIdAsync(
        Guid userId,
        CancellationToken cancellationToken);
}