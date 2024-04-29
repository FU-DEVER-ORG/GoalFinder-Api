using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;

/// <summary>
///     Response mapper for <see cref="ResetPasswordWithOtpResponse"/>
/// </summary>

internal sealed class ResetPasswordWithOtpHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        ResetPasswordWithOtpResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
