using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;

/// <summary>
///  Get user profile by user id http response
/// </summary>

internal sealed class GetUserProfileByUserIdHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetUserProfileResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
