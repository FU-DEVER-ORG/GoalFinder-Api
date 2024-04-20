using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.UserDetail;

public partial interface IUserDetailRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken);
}

