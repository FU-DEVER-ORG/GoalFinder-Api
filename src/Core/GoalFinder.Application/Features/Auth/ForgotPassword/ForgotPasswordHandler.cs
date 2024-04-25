using GoalFinder.Application.Shared.Features;
using GoalFinder.Application.Shared.Tokens.OTP;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

/// <summary>
///     Forgot Password Handler
/// </summary>
internal sealed class ForgotPasswordHandler : IFeatureHandler<
    ForgotPasswordRequest,
    ForgotPasswordResponse>
{
    private readonly UserManager<Data.Entities.User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOtpHandler _otpHandler;

    public ForgotPasswordHandler(
        UserManager<Data.Entities.User> userManager,
        IOtpHandler otpHandler,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _otpHandler = otpHandler;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="command">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<ForgotPasswordResponse> ExecuteAsync(
        ForgotPasswordRequest command,
        CancellationToken ct)
    {
        //Find User By username
        var foundUser = await _userManager.FindByNameAsync(userName: command.UserName);

        //Validate User
        if (Equals(objA: foundUser, objB: default))
        {
            return new()
            {
                StatusCode = ForgotPasswordResponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND
            };
        }

        var isUserTemporarilyRemoved = await _unitOfWork.ForgotPasswordRepository
            .IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct);

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ForgotPasswordResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        //Generate password reset OTP code
        var resetPasswordOTPCode = _otpHandler.Generate(length: 4);

        //Sending Feature are currently skipping

        //Add reset password OTP Code to database
        var dbResultAfterAddingOtp = await _unitOfWork.ForgotPasswordRepository
            .AddResetPasswordTokenToDatabaseAsync(
                userId: foundUser.Id,
                passwordResetOtpCode: resetPasswordOTPCode,
                cancellationToken: ct);

        if(!dbResultAfterAddingOtp)
        {
            return new()
            {
                StatusCode = ForgotPasswordResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = ForgotPasswordResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                OtpCode = resetPasswordOTPCode
            }
        };
    }
}
