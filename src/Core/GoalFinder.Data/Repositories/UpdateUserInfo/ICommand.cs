using GoalFinder.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.UpdateUserInfo;

public partial interface IUpdateUserInfoRepository
{
    Task<bool> UpdateUserCommandAsync(
        UserDetail updateUser,
        UserDetail currentUser,
        IEnumerable<Guid> currentPositionIds,
        IEnumerable<Guid> newPositionIds,
        CancellationToken cancellationToken);
}
