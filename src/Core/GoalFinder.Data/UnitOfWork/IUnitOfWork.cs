using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;

namespace GoalFinder.Data.UnitOfWork;

/// <summary>
///     Interface for unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Gets the login repository.
    /// </summary>
    ILoginRepository LoginRepository { get; }

    /// <summary>
    ///     Gets the insert error log repository.
    /// </summary>
    IInsertErrorLogRepository InsertErrorLogRepository { get; }
    /// <summary>
    ///     Gets the forgot password repository.
    /// </summary>
    IForgotPasswordRepository ForgotPasswordRepository { get; }

    /// <summary>
    ///     Gets the register as user repository.
    /// </summary>
    IRegisterAsUserRepository RegisterAsUserRepository { get; }

    /// <summary>
    ///     Gets the reset password with otp repository.
    /// </summary>
    IResetPasswordWithOtpRepository ResetPasswordWithOtpRepository { get; }
}
