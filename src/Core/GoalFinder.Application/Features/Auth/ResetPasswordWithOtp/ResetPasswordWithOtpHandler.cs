﻿using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
///     Reset password with otp handler
/// </summary>

internal class ResetPasswordWithOtpHandler
    : IFeatureHandler<ResetPasswordWithOtpRequest, ResetPasswordWithOtpResponse>
{
    private readonly UserManager<Data.Entities.User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordWithOtpHandler(
        UserManager<Data.Entities.User> userManager,
        IUnitOfWork unitOfWork
    )
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResetPasswordWithOtpResponse> ExecuteAsync(
        ResetPasswordWithOtpRequest command,
        CancellationToken ct
    )
    {
        // Checking matching new password and confirm password
        if (!Equals(objA: command.newPassword, objB: command.confirmPassword))
        {
            return new()
            {
                StatusCode =
                    ResetPasswordWithOtpResponseStatusCode.NEW_PASSWORD_NOT_MATCH_CONFIRM_PASSWORD
            };
        }

        // get OTP from database
        var otpCode = await _unitOfWork.ResetPasswordWithOtpRepository.FindUserTokenByOtpCodeAsync(
            command.OtpCode,
            cancellationToken: ct
        );

        if (Equals(objA: otpCode, objB: default))
        {
            return new() { StatusCode = ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_FOUND };
        }

        // checking the otp code expired or not?
        if (otpCode.ExpiredAt < DateTime.UtcNow)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.OTP_CODE_IS_EXPIRED
            };
        }

        var isUserTemporarilyRemoved =
            await _unitOfWork.ResetPasswordWithOtpRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: otpCode.UserId,
                cancellationToken: ct
            );

        // checking user is active or not
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.USER_IS_TEMPORARY_REMOVED
            };
        }

        // get user from otp code ?
        var foundUser = await _userManager.FindByIdAsync(userId: otpCode.UserId.ToString());

        var isNewPasswordMatchOldPassword = await _userManager.CheckPasswordAsync(
            foundUser,
            command.confirmPassword
        );
        if (isNewPasswordMatchOldPassword)
        {
            return new()
            {
                StatusCode =
                    ResetPasswordWithOtpResponseStatusCode.NEW_PASSWORD_CANT_BE_MATCH_WITH_OLD_PASSWORD
            };
        }

        // update new user password
        var resetPasswordResult = await _userManager.ResetPasswordAsync(
            user: foundUser,
            token: otpCode.Value,
            newPassword: command.newPassword
        );
        // reset password failed
        if (!resetPasswordResult.Succeeded)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.DATABASE_OPERATION_FAILED
            };
        }

        var removeOtpCodeResult =
            await _unitOfWork.ResetPasswordWithOtpRepository.RemoveUserTokenUsingForResetPasswordAsync(
                otpCode.Value,
                ct
            );

        if (!removeOtpCodeResult)
        {
            return new()
            {
                StatusCode = ResetPasswordWithOtpResponseStatusCode.DATABASE_OPERATION_FAILED
            };
        }

        return new() { StatusCode = ResetPasswordWithOtpResponseStatusCode.OPERATION_SUCCESS };
    }
}
