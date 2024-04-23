using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ForgotPassword;

/// <summary>
/// Interface for forgot password repository
/// </summary>
public partial interface IForgotPasswordRepository
{
    Task<bool> AddResetPasswordTokenToDatabaseAsync(
        Guid userId,
        string passwordResetOtpCode,
        CancellationToken cancellationToken);
}
