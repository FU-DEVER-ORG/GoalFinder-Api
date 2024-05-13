using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.GetMatchesUpcoming;

/// <summary>
///     Response for get matches upcoming features.
/// </summary>
public sealed class GetMatchesUpcomingResponse : IFeatureResponse
{
    public GetMatchesUpcomingResponseStatusCode StatusCode { get; init; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {
        public IEnumerable<Match> MatchesUpcoming { get; set; }

        public sealed class Match
        {
            public string Description { get; init; }

            public DateTime StartTime { get; init; }

            public int CurrentPlayers { get; init; }

            public int MaxMatchPlayersNeed { get; init; }
        }
    }
}
