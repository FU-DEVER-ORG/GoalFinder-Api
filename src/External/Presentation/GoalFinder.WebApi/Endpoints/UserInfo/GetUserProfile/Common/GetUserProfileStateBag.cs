namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Common;

/// <summary>
///     Represents the GetUserProfile state bag.
/// </summary>
internal sealed class GetUserProfileStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
