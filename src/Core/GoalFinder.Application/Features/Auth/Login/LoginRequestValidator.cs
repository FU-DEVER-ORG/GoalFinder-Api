using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Login request validator.
/// </summary>
public sealed class LoginRequestValidator : FeatureRequestValidator<LoginRequest, LoginResponse>
{
    public LoginRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.Username)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.UserName.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.UserName.MinLength);

        RuleFor(expression: request => request.Password)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .Matches(expression: @"^(?=.*\d)(?=.*[A-Z]).+$")
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.Password.MinLength);
    }
}
