using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;

/// <summary>
///     Get User Profile Request Validator
/// </summary>
public sealed class GetUserProfileRequestValidator
    : FeatureRequestValidator<GetUserProfileRequest, GetUserProfileResponse>
{
    public GetUserProfileRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.NickName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(maximumLength: Data.Entities.UserDetail.MetaData.NickName.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.NickName.MinLength);
    }
}
