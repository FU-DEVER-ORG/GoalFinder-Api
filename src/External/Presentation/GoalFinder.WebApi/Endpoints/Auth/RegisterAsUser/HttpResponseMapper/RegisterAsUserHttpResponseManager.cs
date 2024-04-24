using GoalFinder.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Http response manager for register as user feature.
/// </summary>
internal sealed class RegisterAsUserHttpResponseManager
{
    private readonly Dictionary<
        RegisterAsUserResponseStatusCode,
        Func<
            RegisterAsUserRequest,
            RegisterAsUserResponse,
            RegisterAsUserHttpResponse>>
                _dictionary;

    internal RegisterAsUserHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.USER_PASSWORD_IS_NOT_VALID,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.USER_IS_EXISTED,
            value: (request, response) => new()
            {
                HttpCode = StatusCodes.Status409Conflict,
                AppCode = response.StatusCode.ToAppCode(),
            });
    }

    internal Func<
        RegisterAsUserRequest,
        RegisterAsUserResponse,
        RegisterAsUserHttpResponse>
            Resolve(RegisterAsUserResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
