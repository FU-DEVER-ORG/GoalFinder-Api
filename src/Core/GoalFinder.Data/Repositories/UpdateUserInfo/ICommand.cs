using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.UpdateUserInfo;

public partial interface IUpdateUserInfoRepository
{
    Task<bool> UpdateUserCommandAsync(
        UserDetail updateUser,
        UserDetail currentUser,
        IEnumerable<Guid> currentPositionIds,
        IEnumerable<Guid> newPositionIds,
        CancellationToken cancellationToken
    );
}
