using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Enum.GetAllExperiences;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class GetAllExperiencesHttpResponseManager
{
    private readonly Dictionary<
        GetAllExperiencesResponseStatusCode,
        Func<GetAllExperiencesRequest, GetAllExperiencesResponse, GetAllExperiencesHttpResponse>
    > _dictionary;

    internal GetAllExperiencesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllExperiencesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: GetAllExperiencesResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetAllExperiencesRequest,
        GetAllExperiencesResponse,
        GetAllExperiencesHttpResponse
    > Resolve(GetAllExperiencesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
