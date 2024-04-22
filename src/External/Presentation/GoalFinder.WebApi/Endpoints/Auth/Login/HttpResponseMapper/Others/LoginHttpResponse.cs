using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.Auth.Login;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;

/// <summary>
///     Implementation for login http response.
/// </summary>
public class LoginHttpResponse
{
    public int HttpCode { get; init; }

    public string AppCode { get; init; } = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
