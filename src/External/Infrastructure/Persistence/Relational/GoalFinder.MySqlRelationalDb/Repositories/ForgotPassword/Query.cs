using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;

/// <summary>
///     Implementation of <see cref="IForgotPasswordQuery"/>
/// </summary>
internal sealed partial class ForgotPasswordRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue,
            cancellationToken: cancellationToken
        );
    }
}
