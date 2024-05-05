using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

public sealed class GetUserProfileByUserIdRequestValidator
    : FeatureRequestValidator<GetUserProfileByUserIdRequest, GetUserProfileByUserIdResponse>
{
    public GetUserProfileByUserIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.Id)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .NotNull();
    }
}
