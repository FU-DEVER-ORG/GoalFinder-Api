using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using GoalFinder.Application.Others.Commons;
using Microsoft.EntityFrameworkCore;
using GoalFinder.Data.Entities;

namespace GoalFinder.MySqlRelationalDb.Repositories.Login;

internal partial class LoginRepository
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

    public async Task<string> GetUserAvatarUrlQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var foundUserDetail = await _userDetails
            .AsNoTracking()
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(userDetail => new UserDetail
            {
                AvatarUrl = userDetail.AvatarUrl
            })
            .FirstOrDefaultAsync();

        return foundUserDetail.AvatarUrl;
    }
}
