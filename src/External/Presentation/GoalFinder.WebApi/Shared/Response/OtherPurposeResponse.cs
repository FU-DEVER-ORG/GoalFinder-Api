using System;
using System.Collections.Generic;
using GoalFinder.WebApi.Shared.AppCodes;

namespace GoalFinder.WebApi.Shared.Response;

/// <summary>
///     Contain common response for all api.
/// </summary>
/// <remarks>
///     All http responses format must be this format.
/// </remarks>
internal sealed class OtherPurposeResponse
{
    public object Body { get; init; } = new();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public string AppCode { get; init; } = OtherPurposeAppCode.SUCCESS.ToString();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
