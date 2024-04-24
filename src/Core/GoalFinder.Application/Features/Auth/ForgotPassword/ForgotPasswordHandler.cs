using GoalFinder.Application.Shared.Features;
using GoalFinder.Application.Shared.Mail;
using GoalFinder.Application.Shared.Tokens.OTP;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

/// <summary>
/// Forgot Password Handler
/// </summary>
///
internal sealed class ForgotPasswordHandler :
    IFeatureHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly UserManager<Data.Entities.User> _userManager;
    private readonly ISendingMailHandler _sendingMailHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOtpHandler _otpHandler;
    /// <summary>
    /// Forgot Password Handler
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="sendingMailHandler"></param>
    /// <param name="otpHandler"></param>
    /// <param name="unitOfWork"></param>
    public ForgotPasswordHandler(
        UserManager<User> userManager,
        ISendingMailHandler sendingMailHandler,
        IOtpHandler otpHandler,
        IUnitOfWork unitOfWork
        )
    {
        _userManager = userManager;
        _sendingMailHandler = sendingMailHandler;
        _otpHandler = otpHandler;
        _unitOfWork = unitOfWork;
    }
    /// <summary>
    ///  Execute forgot password handler
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<ForgotPasswordResponse> ExecuteAsync(
        ForgotPasswordRequest command,
        CancellationToken ct)
    {
        //Find User By UserName
        var foundUser = await _userManager.FindByNameAsync(userName: command.UserName);

        //Validate User
        if (Equals(objA: foundUser, objB: default(User)))
        {
            return new()
            {
                StatusCode = ForgotPasswordReponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND
            };
        }
        var isUserTemporarilyRemoved = await
           _unitOfWork.ForgotPasswordRepository
           .IsUserTemporarilyRemovedQueryAsync(userId: foundUser.Id, cancellationToken: ct);

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ForgotPasswordReponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        //Generate password reset OTP code
        var resetPasswordOTPCode = _otpHandler.Generate(4);

        //Sending Feature are currently skipping

        //Add reset password OTP Code to database
        var dbResultAfterAddingOtp = await
            _unitOfWork.ForgotPasswordRepository
            .AddResetPasswordTokenToDatabaseAsync(
                foundUser.Id, resetPasswordOTPCode, ct);

        if(!dbResultAfterAddingOtp)
        {
            return new()
            {
                StatusCode = ForgotPasswordReponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }
        return new()
        {
            StatusCode = ForgotPasswordReponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                OtpCode = resetPasswordOTPCode
            }
        };
    }
}
