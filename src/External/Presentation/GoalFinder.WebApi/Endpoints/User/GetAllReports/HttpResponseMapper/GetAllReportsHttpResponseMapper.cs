using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;
using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.User.GetAllReports;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;

/// <summary>
///     Implementation for GetAllReports http response.
/// </summary>
internal sealed class GetAllReportsHttpResponseMapper
{
    private readonly Dictionary<
        GetAllReportsStatusCode,
        Func<GetAllReportsRequest, GetAllReportsResponse, GetAllReportsHttpResponse>
    > _dictionary;

    internal GetAllReportsHttpResponseMapper()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllReportsStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetAllReportsStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<GetAllReportsRequest, GetAllReportsResponse, GetAllReportsHttpResponse> Resolve(
        GetAllReportsStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
