using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;

/// <summary>
///     Http response manager for GetUserProfile feature.
/// </summary>
internal sealed class GetUserProfileHttpResponseManager
{
    private readonly Dictionary<
        GetUserProfileResponseStatusCode,
        Func<
            GetUserProfileRequest,
            GetUserProfileResponse,
            GetUserProfileHttpResponse>>
                _dictionary;

    internal GetUserProfileHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserProfileResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: GetUserProfileResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: GetUserProfileResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode(),
                Body = response.ResponseBody
            });

        _dictionary.Add(
            key: GetUserProfileResponseStatusCode.USER_IS_NOT_FOUND,
             value: (_, response) => new()
             {
                 HttpCode = StatusCodes.Status404NotFound,
                 AppCode = response.StatusCode.ToAppCode()
             });

        _dictionary.Add(
            key: GetUserProfileResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status403Forbidden,
                AppCode = response.StatusCode.ToAppCode()
            });
    }

    internal Func<
        GetUserProfileRequest,
        GetUserProfileResponse,
        GetUserProfileHttpResponse>
            Resolve(GetUserProfileResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
