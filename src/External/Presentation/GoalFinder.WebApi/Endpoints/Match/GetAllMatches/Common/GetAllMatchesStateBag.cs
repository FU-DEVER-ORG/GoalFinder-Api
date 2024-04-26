namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Common;

/// <summary>
///     Represents the GetAllMatches state bag.
/// </summary>
internal sealed class GetAllMatchesStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
