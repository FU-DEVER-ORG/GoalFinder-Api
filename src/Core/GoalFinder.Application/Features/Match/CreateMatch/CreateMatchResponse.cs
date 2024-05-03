using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.CreateMatch;

/// <summary>
///     Create match response.
/// </summary>
public sealed class CreateMatchResponse : IFeatureResponse
{
    public CreateMatchResponseStatusCode StatusCode { get; init; }
}
