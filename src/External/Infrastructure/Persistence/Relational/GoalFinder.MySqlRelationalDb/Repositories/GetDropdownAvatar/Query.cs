using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetDropdownAvatar;

internal partial class GetDropdownAvatarRepository
{
    public Task<UserDetail> GetUserDetailByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _userDetails
            .Where(userDetail => userDetail.UserId == userId)
            .AsNoTrackingWithIdentityResolution()
            .Select(userDetail => new UserDetail
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                User = new() { UserName = userDetail.User.UserName }
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserFoundByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails.AnyAsync(
            predicate: userDetail => userDetail.UserId == userId,
            cancellationToken: cancellationToken
        );
    }

}
