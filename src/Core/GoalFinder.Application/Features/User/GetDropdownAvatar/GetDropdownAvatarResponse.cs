using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.GetDropdownAvatar;

/// <summary>
///     Response for get user dropdown avatar features.
/// </summary>
public sealed class GetDropdownAvatarResponse : IFeatureResponse
{
    public GetDropdownAvatarResponseStatusCode StatusCode { get; init; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {
        public string UserName { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
