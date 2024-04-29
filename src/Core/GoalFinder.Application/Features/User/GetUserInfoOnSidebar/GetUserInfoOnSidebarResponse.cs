using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

/// <summary>
///     Response for get user info on sidebar features.
/// </summary>
public sealed class GetUserInfoOnSidebarResponse : IFeatureResponse
{
    public GetUserInfoOnSidebarResponseStatusCode StatusCode { get; init; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {
        public string UserName { get; init; }

        public string Area { get; init; }

        public int PrestigeScore { get; init; }
    }
}
