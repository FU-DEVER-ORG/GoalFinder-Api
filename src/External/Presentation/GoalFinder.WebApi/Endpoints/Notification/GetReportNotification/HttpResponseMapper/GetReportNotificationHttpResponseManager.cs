using System;
using System.Collections.Generic;
using GoalFinder.Application.Features.Notification.GetReportNotification;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.HttpResponseMapper;

/// <summary>
///     Http response manager for get report notification feature.
/// </summary>
internal sealed class GetReportNotificationHttpResponseManager
{
    private readonly Dictionary<
        GetReportNotificationResponseStatusCode,
        Func<GetReportNotificationRequest, GetReportNotificationResponse, GetReportNotificationHttpResponse>
    > _dictionary;

    internal GetReportNotificationHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetReportNotificationResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetReportNotificationResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<GetReportNotificationRequest, GetReportNotificationResponse, GetReportNotificationHttpResponse> Resolve(
        GetReportNotificationResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
