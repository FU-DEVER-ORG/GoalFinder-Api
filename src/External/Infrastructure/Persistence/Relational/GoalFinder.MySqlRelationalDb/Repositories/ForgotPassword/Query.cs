using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using GoalFinder.Application.Shared.Commons;

namespace GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;

internal sealed partial class ForgotPasswordRepository 
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        return _userDetails
         .Where(predicate:
             userDetail => userDetail.UserId == userId &&
             userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
             userDetail.RemovedAt != DateTime.MinValue)
         .AnyAsync(cancellationToken: cancellationToken);
    }
}
