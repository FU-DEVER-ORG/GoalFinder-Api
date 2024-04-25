using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using System.Text.Json.Serialization;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;

/// <summary>
///     Implementation for GetUserProfile http response.
/// </summary>
public class GetUserProfileHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetUserProfileResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
