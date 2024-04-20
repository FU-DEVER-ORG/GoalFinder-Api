using FuDever.WebApi.AppCodes;
using System;
using System.Collections.Generic;

namespace GoalFinder.WebApi.Others.Commons;

/// <summary>
///     Contain common response for all api.
/// </summary>
/// <remarks>
///     All http responses format must be this format.
/// </remarks>
internal sealed class CommonResponse
{
    public object Body { get; init; } = new();

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public string AppCode { get; init; } = OtherAppCode.SUCCESS.ToString();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
