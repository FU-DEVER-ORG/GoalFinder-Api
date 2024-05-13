using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Common;

/// <summary>
///     Represents the state bag used for
///     the report user after match flow
/// </summary>
internal sealed class ReportUserAfterMatchStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal ReportUserAfterMatchRequest AppRequest { get; } = new();
}
