using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.CreateMatch;

/// <summary>
///     Create match request.
/// </summary>
public sealed class CreateMatchRequest : IFeatureRequest<CreateMatchResponse>
{
    private Guid _hostId { get; set; }

    public string PitchAddress { get; init; }

    public int MaxMatchPlayersNeed { get; init; }

    public decimal PitchPrice { get; init; }

    public string Description { get; init; }

    public int MinPrestigeScore { get; init; }

    public string Address { get; init; }

    public DateTime StartTime { get; init; }

    public Guid CompetitionLevelId { get; init; }

    public void SetHostId(Guid hostId)
    {
        _hostId = hostId;
    }

    public Guid GetHostId()
    {
        return _hostId;
    }
}
