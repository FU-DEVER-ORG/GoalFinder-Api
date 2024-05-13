using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.GetAllReports;

/// <summary>
///     Get All Reports Response
/// </summary>
public sealed class GetAllReportsResponse : IFeatureResponse
{
    public GetAllReportsStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public Match FootballMatch { get; init; }
        public IEnumerable<MatchPlayerDetails> MatchPlayers { get; init; }

        public sealed class MatchPlayerDetails
        {
            public Guid PlayerId { get; init; }

            public int NumberOfReports { get; init; }
        }

        public sealed class Match
        {
            public string PitchAddress { get; init; }

            public int MaxMatchPlayersNeed { get; init; }

            public decimal PitchPrice { get; init; }

            public string Description { get; init; }

            public string StartTime { get; init; }
            public string EndTime { get; init; }

            public string Address { get; set; }

            public string CompetitionLevel { get; init; }
        }
    }
}
