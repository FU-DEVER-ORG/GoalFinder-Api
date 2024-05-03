using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.User.GetDropdownAvatar;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class GetDropdownAvatarHttpResponseManager
{
    private readonly Dictionary<
        GetDropdownAvatarResponseStatusCode,
        Func<
            GetDropdownAvatarRequest,
            GetDropdownAvatarResponse,
            GetDropdownAvatarHttpResponse
        >
    > _dictionary;

    internal GetDropdownAvatarHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetDropdownAvatarResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: GetDropdownAvatarResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetDropdownAvatarResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );


        _dictionary.Add(
            key: GetDropdownAvatarResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetDropdownAvatarRequest,
        GetDropdownAvatarResponse,
        GetDropdownAvatarHttpResponse
    > Resolve(GetDropdownAvatarResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
