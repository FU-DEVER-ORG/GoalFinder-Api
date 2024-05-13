using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;

/// <summary>
///     The response mapper for <see cref="RefreshAccessTokenResponse"/>
/// </summary>

internal sealed class RefreshAccessTokenHttpResponseManager
{
    private readonly Dictionary<
        RefreshAccessTokenResponseStatusCode,
        Func<RefreshAccessTokenRequest, RefreshAccessTokenResponse, RefreshAccessTokenHttpResponse>
    > _dictionary;

    internal RefreshAccessTokenHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.UN_AUTHORIZED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.REQUIRE_LOGIN_AGAIN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RefreshAccessTokenRequest,
        RefreshAccessTokenResponse,
        RefreshAccessTokenHttpResponse
    > Resolve(RefreshAccessTokenResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
