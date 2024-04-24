
using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.Login;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;

/// <summary>
/// Mapper for forgot password 
/// </summary>
internal sealed class ForgotPasswordHttpResponseManager
{
    /// <summary>
    /// Dictionary
    /// </summary>
    private readonly Dictionary<
        ForgotPasswordReponseStatusCode,
        Func<
            ForgotPasswordRequest,
            ForgotPasswordResponse,
            ForgotPasswordHttpReponse>>
                _dictionary;
    /// <summary>
    /// Constructor
    /// </summary>
    internal ForgotPasswordHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
           key: ForgotPasswordReponseStatusCode.OPERATION_SUCCESS,
           value: (_, response) => new OperationSuccessHttpResponse(response: response));

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND,
            value: (_, response) => new UserWithEmailNotFoundHttpsResponse( request: _, response: response));

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.USER_IS_NOT_VERIFY,
            value: (_, response) => new UserIsNotVerifyHttpResponse(request: _,response: response));

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) => new InputValidationFailHttpResponse(response: response));

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) => new UserIsTemporarilyRemovedHttpResponse(request: _,response: response));

        _dictionary.Add(
            key: ForgotPasswordReponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) => new DatabaseOperationFailHttpResponse(response: response));
    }
    /// <summary>
    /// Resolve
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    internal Func<
       ForgotPasswordRequest,
       ForgotPasswordResponse,
       ForgotPasswordHttpReponse>
           Resolve(ForgotPasswordReponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
