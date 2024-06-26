﻿using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

/// <summary>
///     Forgot Password Request Validator
/// </summary>
public sealed class ForgotPasswordRequestValidator
    : FeatureRequestValidator<ForgotPasswordRequest, ForgotPasswordResponse>
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
