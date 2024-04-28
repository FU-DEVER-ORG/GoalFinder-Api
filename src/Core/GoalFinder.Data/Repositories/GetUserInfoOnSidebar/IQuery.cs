using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetUserInfoOnSidebar;

public partial interface IGetUserInfoOnSidebarRepository
{
    Task<bool> IsUserFoundByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
}
