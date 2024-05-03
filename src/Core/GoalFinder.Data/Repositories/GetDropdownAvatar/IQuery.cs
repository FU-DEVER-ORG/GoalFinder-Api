using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetDropdownAvatar;

/// <summary>
///     Interface for dropdown avatar query Repository.
/// </summary>
public partial interface IGetDropdownAvatarRepository
{
    Task<bool> IsUserFoundByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
