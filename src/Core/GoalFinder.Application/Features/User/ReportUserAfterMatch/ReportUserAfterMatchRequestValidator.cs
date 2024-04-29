using FluentValidation;
using GoalFinder.Application.Shared.Features;

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

        RuleFor(expression: request => request.PrestigeScore)
                .Cascade(cascadeMode: CascadeMode.Stop)
                .NotEmpty()
                .LessThanOrEqualTo(Data.Entities.UserDetail.MetaData.PrestigeScore.MaxValue)
                .GreaterThanOrEqualTo(Data.Entities.UserDetail.MetaData.PrestigeScore.MinValue);
    }
}