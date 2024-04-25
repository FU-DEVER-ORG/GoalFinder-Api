using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
///     Reset password with otp handler
/// </summary>

internal class ResetPasswordWithOtpHandler :
    IFeatureHandler<ResetPasswordWithOtpRequest, ResetPasswordWithOtpResponse>
{
    private readonly UserManager<GoalFinder.Data.Entities.User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordWithOtpHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResetPasswordWithOtpResponse> ExecuteAsync(
        ResetPasswordWithOtpRequest command,
        CancellationToken ct)
    {
        // Checking matching new password and confirm password
        if (!Equals(objA: command.newPassword, objB: command.confirmPassword))
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.NEW_PASSWORD_NOT_MATCH_CONFIRM_PASSWORD
            };
        }

        // get OTP from database
        var OtpCode = await _unitOfWork.ResetPasswordWithOtpRepository
            .FindUserTokenByOtpCodeAsync(command.OtpCode, cancellationToken: ct);
        if (OtpCode is null)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_FOUND
            };
        }

        // checking the otp code expired or not?
        var isOtpCodeExpired = await _unitOfWork.ResetPasswordWithOtpRepository
            .IsOtpCodeForResettingPasswordExpiredAsync(OtpCode.Value, ct);
        if (isOtpCodeExpired)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.OTP_CODE_IS_EXPIRED
            };
        }

        // get user from otp code ?

        var foundUser = await _userManager.FindByIdAsync(userId: OtpCode.UserId.ToString());

        var isUserTemporarilyRemoved = await _unitOfWork.ResetPasswordWithOtpRepository
            .IsUserTemporarilyRemovedQueryAsync(userId: foundUser.Id, cancellationToken: ct);

        // checking user is active or not
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.USER_IS_TEMPORARY_REMOVED
            };
        }

        // update new user password
        var resetPasswordResult = await _userManager.ResetPasswordAsync(
            user: foundUser,
            token: OtpCode.Value,
            newPassword: command.newPassword
            );
        // reset password failed
        if (!resetPasswordResult.Succeeded)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.DATABASE_OPERATION_FAILD
            };
        }

        var removeOtpCodeResult = await _unitOfWork.ResetPasswordWithOtpRepository
            .RemoveUserTokenUsingForResetPasswordAsync(OtpCode.Value, ct);

        if(!removeOtpCodeResult)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.DATABASE_OPERATION_FAILD
            };
        }

        return new()
        {
            StatusCode = ResetPasswordWithOtpResponseStatusCode.OPERATION_SUCCESS
        };
    }
}