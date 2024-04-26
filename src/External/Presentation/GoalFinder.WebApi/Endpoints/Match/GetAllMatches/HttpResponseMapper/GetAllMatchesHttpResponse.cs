using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.Match.GetAllMatches;
using System.Text.Json.Serialization;

namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;

/// <summary>
///     Implementation for GetAllMatches http response.
/// </summary>
public class GetAllMatchesHttpResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetAllMatchesResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
