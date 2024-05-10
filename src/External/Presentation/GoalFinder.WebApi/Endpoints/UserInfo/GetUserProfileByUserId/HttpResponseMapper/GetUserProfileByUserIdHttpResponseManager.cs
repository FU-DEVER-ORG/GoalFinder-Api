using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;

/// <summary>
/// Get user profile by user id http response
/// </summary>

internal sealed class GetUserProfileByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetUserProfileByUserIdResponseStatusCode,
        Func<
            GetUserProfileByUserIdRequest,
            GetUserProfileByUserIdResponse,
            GetUserProfileByUserIdHttpResponse
        >
    > _dictionary;

    public GetUserProfileByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserProfileByUserIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetUserProfileByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetUserProfileByUserIdResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetUserProfileByUserIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetUserProfileByUserIdRequest,
        GetUserProfileByUserIdResponse,
        GetUserProfileByUserIdHttpResponse
    > Resolve(GetUserProfileByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
