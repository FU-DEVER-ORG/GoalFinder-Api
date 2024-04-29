using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
///      Report user after match response
/// </summary>
public sealed class ReportUserAfterMatchResponse : IFeatureResponse
{
    public ReportUserAfterMatchResponseStatusCode StatusCode { get; set; }
}
