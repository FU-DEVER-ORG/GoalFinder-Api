using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;

/// <summary>
///     Manager for <see cref="ResetPasswordWithOtpResponse"/>
/// </summary>

internal sealed class ResetPasswordWithOtpHttpResponseManager
{
    /// <summary>
    ///     Dictionary for <see cref="ResetPasswordWithOtpResponse"/>
    /// </summary>
    private readonly Dictionary<
        ResetPasswordWithOtpResponseStatusCode,
        Func<
            ResetPasswordWithOtpRequest,
            ResetPasswordWithOtpResponse,
            ResetPasswordWithOtpHttpResponse>>
                _dictionary;

    /// <summary>
    ///     Constructor for <see cref="ResetPasswordWithOtpHttpResponseManager"/>
    /// </summary>
    internal ResetPasswordWithOtpHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_FOUND,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_VALID,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_IS_EXPIRED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status417ExpectationFailed,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.INPUT_VALIDATION_FAILED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.NEW_PASSWORD_NOT_MATCH_CONFIRM_PASSWORD,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status417ExpectationFailed,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.USER_IS_TEMPORARY_REMOVED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status417ExpectationFailed,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.INPUT_NOT_UNDERSTANDABLE,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });
        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.NEW_PASSWORD_CANT_BE_MATCH_WITH_OLD_PASSWORD,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });
    }

    internal Func<
            ResetPasswordWithOtpRequest,
            ResetPasswordWithOtpResponse,
            ResetPasswordWithOtpHttpResponse>
            Resolve(ResetPasswordWithOtpResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}