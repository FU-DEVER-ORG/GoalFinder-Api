using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class GetAllCompetitionLevelsHttpResponseManager
{
    private readonly Dictionary<
        GetAllCompetitionLevelsResponseStatusCode,
        Func<
            GetAllCompetitionLevelsRequest,
            GetAllCompetitionLevelsResponse,
            GetAllCompetitionLevelsHttpResponse
        >
    > _dictionary;

    internal GetAllCompetitionLevelsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllCompetitionLevelsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.Body
                }
        );

        _dictionary.Add(
            key: GetAllCompetitionLevelsResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetAllCompetitionLevelsRequest,
        GetAllCompetitionLevelsResponse,
        GetAllCompetitionLevelsHttpResponse
    > Resolve(GetAllCompetitionLevelsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
