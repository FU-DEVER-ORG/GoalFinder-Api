
using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

/// <summary>
///     Mapper for forgot password
/// </summary>
internal sealed class ForgotPasswordHttpResponseManager
{
    private readonly Dictionary<
        ForgotPasswordReponseStatusCode,
        Func<
            ForgotPasswordRequest,
            ForgotPasswordResponse,
            ForgotPasswordHttpReponse>>
                _dictionary;

    internal ForgotPasswordHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode(),
                Body = response.ResponseBody
            });

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"User with user email {_.UserName} is not found!"]
            });

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"Input validation failed!"]
            });

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status403Forbidden,
                AppCode = response.StatusCode.ToAppCode(),
                ErrorMessages = [$"User with user email {_.UserName} is ban or temporarily removed by admintrator!"]
            });

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode(),
            });
    }
    /// <summary>
    /// Resolve
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    internal Func<
        ForgotPasswordRequest,
        ForgotPasswordResponse,
        ForgotPasswordHttpReponse>
            Resolve(ForgotPasswordReponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
