
using GoalFinder.Application.Features.Match.GetMatchesUpcoming;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Common;

/// <summary>
///     Represents the state bag used for
///     the get matches upcoming flow.
/// </summary>
internal sealed class GetMatchesUpcomingStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetMatchesUpcomingRequest AppRequest { get; } = new();
}
