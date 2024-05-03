namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch.Common;

/// <summary>
///     Represents the state bag used for
///     the create match flow.
/// </summary>
internal sealed class CreateMatchStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
