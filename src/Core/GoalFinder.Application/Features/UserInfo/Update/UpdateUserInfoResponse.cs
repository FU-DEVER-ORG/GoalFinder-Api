using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.Update;

    public sealed class UpdateUserInfoResponse : IFeatureResponse
    {
        public UpdateUserInfoResponseStatusCode StatusCode { get; init; }

    }

