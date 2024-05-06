using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Enum.GetAllPositions;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class GetAllPositionsHttpResponseManager
{
    private readonly Dictionary<
        GetAllPositionsResponseStatusCode,
        Func<GetAllPositionsRequest, GetAllPositionsResponse, GetAllPositionsHttpResponse>
    > _dictionary;

    internal GetAllPositionsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllPositionsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: GetAllPositionsResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetAllPositionsRequest,
        GetAllPositionsResponse,
        GetAllPositionsHttpResponse
    > Resolve(GetAllPositionsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
