using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Validator for RefreshAccessToken
/// </summary>

public sealed class RefreshAccessTokenValidator
    : FeatureRequestValidator<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    public RefreshAccessTokenValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.RefreshToken).NotEmpty().NotNull();
    }
}
