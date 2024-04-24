using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using static GoalFinder.Data.Entities.ErrorLogging.MetaData;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others
{
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
                key: UpdateUserInfoResponseStatusCode.USERNAME_IS_EXISTED,
                value: (request, response) => new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode(),
                    ErrorMessages =
                [
                    $"User with username = {request.UserName} already exists"
                ]
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
}
