using System.Threading.Tasks;
using System.Threading;

using System;

namespace GoalFinder.Data.Repositories.Login;

public partial interface ILoginRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<string> GetUserAvatarUrlQueryAsync(
        Guid userId,
        CancellationToken cancellationToken);
}