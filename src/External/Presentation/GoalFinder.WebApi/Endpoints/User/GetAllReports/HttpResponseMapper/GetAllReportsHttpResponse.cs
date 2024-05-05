using GoalFinder.Application.Features.User.GetAllReports;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;

/// <summary>
///     Implementation for GetAllReports http response.
/// </summary>
public class GetAllReportsHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = 
        GetAllReportsStatusCode
        .OPERATION_SUCCESS
        .ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
