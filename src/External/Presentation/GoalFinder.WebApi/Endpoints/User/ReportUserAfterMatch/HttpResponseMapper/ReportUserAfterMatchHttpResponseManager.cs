using System;
using System.Collections.Generic;
using System.Net;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;

internal sealed class ReportUserAfterMatchHttpResponseManager
{
    private readonly Dictionary<
        ReportUserAfterMatchResponseStatusCode,
        Func<
            ReportUserAfterMatchRequest,
            ReportUserAfterMatchResponse,
            ReportUserAfterMatchHttpResponse
        >
    > _dictionary;

    internal ReportUserAfterMatchHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.FOOTBALL_MATCH_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.FOOTBALL_MATCH_ENDTIME_IS_STILL_AVAILABLE,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.PLAYER_DOES_NOT_EXIST_IN_FOOTBALL_MATCH,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ReportUserAfterMatchResponseStatusCode.FORM_HAS_EXPIRED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        ReportUserAfterMatchRequest,
        ReportUserAfterMatchResponse,
        ReportUserAfterMatchHttpResponse
    > Resolve(ReportUserAfterMatchResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
