using GoalFinder.Application.Shared.Features;
using FluentValidation;
namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;

/// <summary>
///     Get User Profile Request Validator
/// </summary>
public sealed class GetUserProfileRequestValidator : FeatureRequestValidator<
    GetUserProfileRequest,
    GetUserProfileResponse>
{
    public GetUserProfileRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.UserName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .EmailAddress()
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.UserName.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.UserName.MinLength);
    }
}
