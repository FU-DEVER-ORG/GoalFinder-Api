using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;

/// <summary>
///     The response mapper for <see cref="RefreshAccessTokenResponse"/>
/// </summary>

internal sealed class RefreshAccessTokenHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
