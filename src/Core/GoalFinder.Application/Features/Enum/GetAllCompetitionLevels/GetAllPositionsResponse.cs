using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;

/// <summary>
///     Response for get all competitionLevels features.
/// </summary>
public sealed class GetAllCompetitionLevelsResponse : IFeatureResponse
{
    public GetAllCompetitionLevelsResponseStatusCode StatusCode { get; init; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {
        public IEnumerable<CompetitionLevel> CompetitionLevels { get; set; }

        public sealed class CompetitionLevel
        {
            public Guid CompetitionLevelId { get; init; }

            public string Name { get; init; }
        }
    }
}
