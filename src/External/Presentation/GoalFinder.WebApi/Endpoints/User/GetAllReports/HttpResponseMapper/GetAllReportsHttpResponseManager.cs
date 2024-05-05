using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.User.GetAllReports;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;

/// <summary>
///     Implementation for GetAllReports http response.
/// </summary>
internal sealed class GetAllReportsHttpResponseManager
{
    private readonly Dictionary<
        GetAllReportsStatusCode,
        Func<GetAllReportsRequest, GetAllReportsResponse, GetAllReportsHttpResponse>
    > _dictionary;

    internal GetAllReportsHttpResponseManager()
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

        _dictionary.Add(
            key: GetAllReportsStatusCode.MATCH_ID_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
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
