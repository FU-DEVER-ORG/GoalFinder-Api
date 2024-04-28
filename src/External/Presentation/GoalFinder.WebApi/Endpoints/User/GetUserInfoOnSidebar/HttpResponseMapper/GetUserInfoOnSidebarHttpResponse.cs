using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

namespace GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.HttpResponseMapper;

/// <summary>
///     Represent http response for get user info on sidebar feature.
/// </summary>
internal sealed class GetUserInfoOnSidebarHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetUserInfoOnSidebarResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
