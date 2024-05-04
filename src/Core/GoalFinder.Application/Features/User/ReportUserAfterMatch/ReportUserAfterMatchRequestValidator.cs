using GoalFinder.Application.Shared.Features;
using FluentValidation;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
///       Report user after match request validator
/// </summary>
public sealed class ReportUserAfterMatchRequestValidator
    : FeatureRequestValidator<ReportUserAfterMatchRequest, ReportUserAfterMatchResponse>
{
    public ReportUserAfterMatchRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.UserId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(expression: request => request.FootballMatchId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}