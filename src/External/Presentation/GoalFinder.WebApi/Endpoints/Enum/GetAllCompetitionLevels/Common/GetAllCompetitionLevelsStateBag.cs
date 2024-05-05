using GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Common;

/// <summary>
///     Represents the state bag used for
///     the get all CompetitionLevels flow.
/// </summary>
internal sealed class GetAllCompetitionLevelsStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetAllCompetitionLevelsRequest AppRequest { get; } = new();
}
