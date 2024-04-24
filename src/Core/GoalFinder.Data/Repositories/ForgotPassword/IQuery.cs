using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ForgotPassword;

/// <summary>
///     Interface for ForgotPasswordRepository
/// </summary>
public partial interface IForgotPasswordRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}

