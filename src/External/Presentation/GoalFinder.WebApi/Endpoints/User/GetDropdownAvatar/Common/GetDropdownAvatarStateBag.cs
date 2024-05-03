using GoalFinder.Application.Features.User.GetDropdownAvatar;

namespace GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.Common;

/// <summary>
///     Represents the state bag used for
///     the get dropdown avatar information on navbar flow.
/// </summary>
internal sealed class GetDropdownAvatarStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetDropdownAvatarRequest AppRequest { get; } = new();
}
