﻿using GoalFinder.Application.Features.User.UpdateUserInfo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;

/// <summary>
///     Manages the mapping between app
///     response and http response.
/// </summary>
internal sealed class UpdateUserInfoHttpResponseManager
{
    private readonly Dictionary<
        UpdateUserInfoResponseStatusCode,
        Func<
            UpdateUserInfoRequest,
            UpdateUserInfoResponse,
            UpdateUserInfoHttpResponse>>

            _dictionary;

    internal UpdateUserInfoHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.UPDATE_SUCCESS,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status200OK,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.USER_NOT_FOUND,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = response.StatusCode.ToAppCode()
            });

        _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.USERNAME_IS_ALREADY_TAKEN,
            value: (request, response) => new()
            {
                HttpCode = StatusCodes.Status409Conflict,
                AppCode = response.StatusCode.ToAppCode(),
            });

        _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) => new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = response.StatusCode.ToAppCode()
            });
    }

    internal Func<
        UpdateUserInfoRequest,
        UpdateUserInfoResponse,
        UpdateUserInfoHttpResponse>
        Resolve(UpdateUserInfoResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
