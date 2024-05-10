using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

/// <summary>
/// Get User Profile By User Id
/// </summary>

public sealed class GetUserProfileByUserIdRequestValidator
    : FeatureRequestValidator<GetUserProfileByUserIdRequest, GetUserProfileByUserIdResponse>
{
    public GetUserProfileByUserIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.Id)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty();
    }
}
