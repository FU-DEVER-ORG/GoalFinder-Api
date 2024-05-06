namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Common;

/// <summary>
///     Get User Profile By User Id State Bag
/// </summary>

internal sealed class GetUserProfileByUserIdStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
