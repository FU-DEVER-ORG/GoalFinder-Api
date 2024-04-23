
using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using System.Text.Json.Serialization;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;

public class ForgotPasswordHttpReponse
{
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = ForgotPasswordReponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
