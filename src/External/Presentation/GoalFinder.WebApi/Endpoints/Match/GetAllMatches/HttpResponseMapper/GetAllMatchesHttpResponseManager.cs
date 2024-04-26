using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Match.GetAllMatches;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;

/// <summary>
///     Http response manager for GetAllMatches feature.
/// </summary>
internal sealed class GetAllMatchesHttpResponseManager
{
    private readonly Dictionary<
        GetAllMatchesResponseStatusCode,
        Func<
            GetAllMatchesRequest,
            GetAllMatchesResponse,
            GetAllMatchesHttpResponse>>
                _dictionary;

    internal GetAllMatchesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMatchesResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: GetAllMatchesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode(),
                Body = response.ResponseBody
            });
    }

    internal Func<
        GetAllMatchesRequest,
        GetAllMatchesResponse,
        GetAllMatchesHttpResponse>
            Resolve(GetAllMatchesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
