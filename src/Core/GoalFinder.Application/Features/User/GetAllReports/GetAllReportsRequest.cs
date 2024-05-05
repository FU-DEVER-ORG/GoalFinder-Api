using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.GetAllReports;

/// <summary>
///     Get All Reports Request
/// </summary>
public sealed class GetAllReportsRequest : IFeatureRequest<GetAllReportsResponse>
{
    public Guid MatchId { get; init; }
}
