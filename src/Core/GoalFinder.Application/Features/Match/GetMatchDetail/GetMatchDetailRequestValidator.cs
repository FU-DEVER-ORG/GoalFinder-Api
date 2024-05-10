using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.GetMatchDetail;

/// <summary>
///     Get Match Detail Request Validator
/// </summary>

public sealed class GetMatchDetailRequestValidator
    : FeatureRequestValidator<GetMatchDetailRequest, GetMatchDetailResponse>
{
    public GetMatchDetailRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.MatchId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty();
    }
}
