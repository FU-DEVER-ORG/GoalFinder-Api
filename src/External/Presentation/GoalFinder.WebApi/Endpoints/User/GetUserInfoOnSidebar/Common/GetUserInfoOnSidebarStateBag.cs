using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

namespace GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.Common;

/// <summary>
///     Represents the state bag used for
///     the get user information on sidebar flow.
/// </summary>
internal sealed class GetUserInfoOnSidebarStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetUserInfoOnSidebarRequest AppRequest { get; } = new();
}
