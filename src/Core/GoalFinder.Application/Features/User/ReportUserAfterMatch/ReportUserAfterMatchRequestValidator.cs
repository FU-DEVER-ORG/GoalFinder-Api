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
    }
}