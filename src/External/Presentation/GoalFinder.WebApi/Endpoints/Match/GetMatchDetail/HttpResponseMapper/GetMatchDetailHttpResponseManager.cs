using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Match.GetMatchDetail;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;

internal sealed class GetMatchDetailHttpResponseManager
{
    private readonly Dictionary<
        GetMatchDetailResponseStatusCode,
        Func<GetMatchDetailRequest, GetMatchDetailResponse, GetMatchDetailHttpResponse>
    > _dictionary;

    internal GetMatchDetailHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.OPERATION_SUCCESS,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.UN_AUTHORIZED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.FORBIDDEN,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetMatchDetailResponseStatusCode.INPUT_NOT_UNDERSTANDABLE,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetMatchDetailRequest,
        GetMatchDetailResponse,
        GetMatchDetailHttpResponse
    > Resolve(GetMatchDetailResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
