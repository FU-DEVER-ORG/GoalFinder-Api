using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Register;

/// <summary>
///     Register as user request validator.
/// </summary>
public sealed class RegisterAsUserRequestValidator :
    FeatureRequestValidator<RegisterAsUserRequest, RegisterAsUserResponse>
{
    public RegisterAsUserRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.Email)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.Email.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.Email.MinLength);

        RuleFor(expression: request => request.Password)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .Matches(expression: @"^(?=.*\d)(?=.*[A-Z]).+$")
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.Password.MinLength);
    }
}
