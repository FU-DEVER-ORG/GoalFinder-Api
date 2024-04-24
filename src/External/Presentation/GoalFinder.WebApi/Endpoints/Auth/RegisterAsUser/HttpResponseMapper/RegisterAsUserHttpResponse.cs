using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using GoalFinder.Application.Features.Auth.Register;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Implementation for register as user http response.
/// </summary>
internal sealed class RegisterAsUserHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = RegisterAsUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
