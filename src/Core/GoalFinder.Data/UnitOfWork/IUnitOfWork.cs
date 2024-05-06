using GoalFinder.Data.Repositories.CreateMatch;
using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.Data.Repositories.GetAllMatches;
using GoalFinder.Data.Repositories.GetReportNotification;
using GoalFinder.Data.Repositories.GetUserInfoOnSidebar;
using GoalFinder.Data.Repositories.GetUserProfile;
using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RefreshAccessTokenRepository;
using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using GoalFinder.Data.Repositories.UpdateUserInfo;

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
    ///     Gets the update user information repository.
    /// </summary>

    IUpdateUserInfoRepository UpdateUserInfoRepository { get; }

    /// <summary>
    ///     Gets the user profile repository
    /// </summary>
    IGetUserProfileRepository GetUserProfileRepository { get; }

    /// <summary>
    ///     Gets the reset password with otp repository.
    /// </summary>
    IResetPasswordWithOtpRepository ResetPasswordWithOtpRepository { get; }

    /// <summary>
    ///     Gets all matches repository
    /// </summary>
    IGetAllMatchesRepository GetAllMatchesRepository { get; }

    /// <summary>
    ///     Gets the refresh access token repository
    /// </summary>
    IRefreshAccessTokenRepository RefreshAccessTokenRepository { get; }

    /// <summary>
    ///     Gets the get user info on sidebar repository
    /// </summary>
    IGetUserInfoOnSidebarRepository GetUserInfoOnSidebarRepository { get; }

    /// <summary>
    ///     Gets the create new match repository
    /// </summary>
    ICreateMatchRepository CreateMatchRepository { get; }

    /// <summary>
    ///     Gets the report notification repository
    /// </summary>
    IGetReportNotificationRepository GetReportNotificationRepository { get; }
}
