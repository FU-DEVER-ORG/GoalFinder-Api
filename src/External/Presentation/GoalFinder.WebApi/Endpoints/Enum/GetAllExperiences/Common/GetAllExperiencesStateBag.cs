using GoalFinder.Application.Features.Enum.GetAllExperiences;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.Common;

/// <summary>
///     Represents the state bag used for
///     the get all Experiences on sidebar flow.
/// </summary>
internal sealed class GetAllExperiencesStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetAllExperiencesRequest AppRequest { get; } = new();
}
