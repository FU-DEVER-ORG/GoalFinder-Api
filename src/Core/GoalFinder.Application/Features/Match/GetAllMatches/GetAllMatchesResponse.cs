using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.GetAllMatches;

/// <summary>
///     Get All Football Matches Response.
/// </summary>
public sealed class GetAllMatchesResponse : IFeatureResponse
{
    public GetAllMatchesResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<FootballMatch> FootballMatches { get; init; }

        public sealed class FootballMatch
        {
            public Guid Id { get; init; }

            public string PitchAddress { get; init; }

            public int MaxMatchPlayersNeed { get; init; }

            public decimal PitchPrice { get; init; }

            public string Description { get; init; }

            public int MinPrestigeScore { get; set; }

            public string StartTime { get; init; }

            public string Address { get; set; }

            public string CompetitionLevel { get; init; }

            public string TimeAgo { get; set; }

            public Guid HostId { get; set; }
            
            public string HostName { get; set; }
        }
    }
}
