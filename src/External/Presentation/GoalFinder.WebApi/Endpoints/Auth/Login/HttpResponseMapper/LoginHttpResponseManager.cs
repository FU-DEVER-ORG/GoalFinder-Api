using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Auth.Login;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Http response manager for login feature.
/// </summary>
internal sealed class LoginHttpResponseManager
{
    private readonly Dictionary<
        LoginResponseStatusCode,
        Func<LoginRequest, LoginResponse, LoginHttpResponse>
    > _dictionary;

    internal LoginHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LoginResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.INPUT_NOT_UNDERSTANDABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_LOCKED_OUT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status429TooManyRequests,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<LoginRequest, LoginResponse, LoginHttpResponse> Resolve(
        LoginResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
