using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class GetUserInfoOnSidebarHttpResponseManager
{
    private readonly Dictionary<
        GetUserInfoOnSidebarResponseStatusCode,
        Func<
            GetUserInfoOnSidebarRequest,
            GetUserInfoOnSidebarResponse,
            GetUserInfoOnSidebarHttpResponse
        >
    > _dictionary;

    internal GetUserInfoOnSidebarHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserInfoOnSidebarResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: GetUserInfoOnSidebarResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetUserInfoOnSidebarResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetUserInfoOnSidebarResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetUserInfoOnSidebarResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetUserInfoOnSidebarRequest,
        GetUserInfoOnSidebarResponse,
        GetUserInfoOnSidebarHttpResponse
    > Resolve(GetUserInfoOnSidebarResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
