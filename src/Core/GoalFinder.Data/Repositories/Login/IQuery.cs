﻿using GoalFinder.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.Login;

public partial interface ILoginRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

}
