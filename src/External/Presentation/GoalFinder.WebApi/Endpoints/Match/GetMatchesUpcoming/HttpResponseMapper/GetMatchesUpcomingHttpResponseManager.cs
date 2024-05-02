using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Match.GetMatchesUpcoming;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class GetMatchesUpcomingHttpResponseManager
{
    private readonly Dictionary<
        GetMatchesUpcomingResponseStatusCode,
        Func<
            GetMatchesUpcomingRequest,
            GetMatchesUpcomingResponse,
            GetMatchesUpcomingHttpResponse
        >
    > _dictionary;

    internal GetMatchesUpcomingHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMatchesUpcomingResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: GetMatchesUpcomingResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
        _dictionary.Add(
            key: GetMatchesUpcomingResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetMatchesUpcomingResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetMatchesUpcomingResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetMatchesUpcomingRequest,
        GetMatchesUpcomingResponse,
        GetMatchesUpcomingHttpResponse
    > Resolve(GetMatchesUpcomingResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
