﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.Login;

internal partial class LoginRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .Where(predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue
            )
            .AnyAsync(cancellationToken: cancellationToken);
    }

    public async Task<UserDetail> GetUserDetailByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var foundUserDetail = await _userDetails
            .AsNoTracking()
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(selector: userDetail => new UserDetail()
            {
                AvatarUrl = userDetail.AvatarUrl,
                NickName = userDetail.NickName,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return foundUserDetail;
    }
}
