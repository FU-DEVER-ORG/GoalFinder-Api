using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.CreateMatch;

/// <summary>
///     Create match request validator.
/// </summary>
public sealed class CreateMatchRequestValidator
    : FeatureRequestValidator<CreateMatchRequest, CreateMatchResponse>
{
    public CreateMatchRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PitchAddress)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(
                maximumLength: Data.Entities.FootballMatch.MetaData.PitchAddress.MaxLength
            )
            .MinimumLength(
                minimumLength: Data.Entities.FootballMatch.MetaData.PitchAddress.MinLength
            );

        RuleFor(expression: request => request.MaxMatchPlayersNeed)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .GreaterThan(
                valueToCompare: Data.Entities.FootballMatch.MetaData.MaxMatchPlayersNeed.MinValue
            );

        RuleFor(expression: request => request.PitchPrice)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .GreaterThan(valueToCompare: Data.Entities.FootballMatch.MetaData.PitchPrice.MinValue);

        RuleFor(expression: request => request.Description)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(
                minimumLength: Data.Entities.FootballMatch.MetaData.Description.MinLength
            );

        RuleFor(expression: request => request.MinPrestigeScore)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .LessThan(
                valueToCompare: Data.Entities.FootballMatch.MetaData.MinPrestigeScore.MaxValue
            )
            .GreaterThan(
                valueToCompare: Data.Entities.FootballMatch.MetaData.MinPrestigeScore.MinValue
            );

        RuleFor(expression: request => request.Address)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(maximumLength: Data.Entities.FootballMatch.MetaData.Address.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.FootballMatch.MetaData.Address.MinLength);
    }
}
