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
    /// Dictionary for <see cref="ResetPasswordWithOtpResponse"/>
    /// </summary>

    private readonly Dictionary<
        ResetPasswordWithOtpResponseStatusCode,
        Func<
            ResetPasswordWithOtpRequest,
            ResetPasswordWithOtpResponse,
            ResetPasswordWithOtpHttpResponse>>
                _dictionary;

    /// <summary>
    /// Constructor for <see cref="ResetPasswordWithOtpHttpResponseManager"/>
    /// </summary>

    internal ResetPasswordWithOtpHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.DATABASE_OPERATION_FAILD,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"Database operation failed!"]
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_FOUND,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"OTP code {_.OtpCode} not found!"]
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_VALID,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"OTP code {_.OtpCode} not valid!"]
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_IS_EXPIRED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status417ExpectationFailed,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"OTP code {_.OtpCode} is expired!"]
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.INPUT_VALIDATION_FAILD,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"Input validation failed!"]
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.NEW_PASSWORD_NOT_MATCH_CONFIRM_PASSWORD,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status417ExpectationFailed,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"New password and confirm password not match!"]
            });

        _dictionary.Add(
            key: ResetPasswordWithOtpResponseStatusCode.USER_IS_TEMPORARY_REMOVED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status417ExpectationFailed,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"User is temporary removed by admintrator!"]
            });
    }

    /// <summary>
    /// Resolve <see cref="ResetPasswordWithOtpResponse"/>
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>

    internal Func<
            ResetPasswordWithOtpRequest,
            ResetPasswordWithOtpResponse,
            ResetPasswordWithOtpHttpResponse>
            Resolve(ResetPasswordWithOtpResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}