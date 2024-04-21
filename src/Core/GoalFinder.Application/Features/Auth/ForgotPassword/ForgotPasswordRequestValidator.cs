using GoalFinder.Application.Shared.Features;
using FluentValidation;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordRequestValidator :
    FeatureRequestValidator<ForgotPasswordRequest, ForgotPasswordResponse>
{
    public ForgotPasswordRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(expression: request => request.UserName)
           .Cascade(cascadeMode: CascadeMode.Stop)
           .NotEmpty()
           .EmailAddress()
           .MaximumLength(maximumLength: Data.Entities.User.MetaData.UserName.MaxLength)
           .MinimumLength(minimumLength: Data.Entities.User.MetaData.UserName.MinLength);
    }
}
