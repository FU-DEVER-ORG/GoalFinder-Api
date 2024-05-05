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
        public IEnumerable<MatchPlayer> MatchPlayers { get; init; }

        public sealed class MatchPlayer
        {
            public Guid MatchId { get; init; }
            public Guid PlayerId { get; init; }

            public string PlayerName { get; init; }

            public int NumberOfReports { get; init; }
        }
    }
}
