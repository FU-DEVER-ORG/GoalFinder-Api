using GoalFinder.Application.Shared.Features;
using FluentValidation;

namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
/// Request validator for <see cref="ResetPasswordWithOtpRequest"/>
/// </summary>

public sealed class ResetPasswordWithOtpRequestValidator :
    FeatureRequestValidator<
        ResetPasswordWithOtpRequest,
        ResetPasswordWithOtpResponse>
{

    /// <summary>
    /// Create a new instance of <see cref="ResetPasswordWithOtpRequestValidator"/>
    /// </summary>

    private readonly string _PASSWORD_EXPRESSION = @"^(?=.*\d)(?=.*[A-Z]).+$";

    public ResetPasswordWithOtpRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.OtpCode)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty();

        RuleFor(expression: request => request.newPassword)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .Matches(expression: _PASSWORD_EXPRESSION)
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.Password.MinLength);

        RuleFor(expression: request => request.confirmPassword)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .Matches(expression: _PASSWORD_EXPRESSION)
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.Password.MinLength);
    }
}
