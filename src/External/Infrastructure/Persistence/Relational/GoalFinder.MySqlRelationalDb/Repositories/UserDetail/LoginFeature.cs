using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GoalFinder.Application.Others.Commons;

namespace GoalFinder.MySqlRelationalDb.Repositories.UserDetail;

internal sealed partial class UserDetailRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate:
                userDetail => userDetail.UserId == userId &&
                userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
                userDetail.RemovedAt != DateTime.MinValue)
            .AnyAsync(cancellationToken: cancellationToken);
    }
}
