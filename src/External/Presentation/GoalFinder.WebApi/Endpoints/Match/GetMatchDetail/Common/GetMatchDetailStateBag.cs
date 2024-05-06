namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Common;

/// <summary>
///     State bag for GetMatchDetail
/// </summary>

internal sealed class GetMatchDetailStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
