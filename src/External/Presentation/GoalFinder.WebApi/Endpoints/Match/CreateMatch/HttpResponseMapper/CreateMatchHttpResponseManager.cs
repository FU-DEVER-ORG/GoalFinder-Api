using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Match.CreateMatch;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class CreateMatchHttpResponseManager
{
    private readonly Dictionary<
        CreateMatchResponseStatusCode,
        Func<CreateMatchRequest, CreateMatchResponse, CreateMatchHttpResponse>
    > _dictionary;

    internal CreateMatchHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.USER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.UN_AUTHORIZED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.INPUT_NOT_UNDERSTANDABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: CreateMatchResponseStatusCode.LIMIT_ONE_MATCH_PER_DAY,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

         _dictionary.Add(
            key: CreateMatchResponseStatusCode.PRESTIGE_IS_NOT_ENOUGH,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        CreateMatchRequest,
        CreateMatchResponse,
        CreateMatchHttpResponse
    > Resolve(CreateMatchResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
