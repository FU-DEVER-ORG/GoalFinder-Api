using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;
using System;
using System.Collections.Generic;

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
           value: (_, response) => new UpdateSuccessHttpResponse(response: response));
            _dictionary.Add(
           key: UpdateUserInfoResponseStatusCode.USER_NOT_FOUND,
           value: (_, response) => new UserNotFoundHttpResponse(response: response));
            _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new DatabaseOperationFailHttpResponse(response: response));
            _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.USERNAME_IS_EXISTED,
            value: (request, response) => new UserIsExstedHttpResponse(request: request ,response: response));
            _dictionary.Add(
            key: UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) => new InputValidationFailHttpResponse( response: response));
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
