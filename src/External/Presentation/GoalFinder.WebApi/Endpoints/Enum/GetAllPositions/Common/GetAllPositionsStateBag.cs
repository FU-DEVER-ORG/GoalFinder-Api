using GoalFinder.Application.Features.Enum.GetAllPositions;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Common;

/// <summary>
///     Represents the state bag used for
///     the get all positions on sidebar flow.
/// </summary>
internal sealed class GetAllPositionsStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetAllPositionsRequest AppRequest { get; } = new();
}
