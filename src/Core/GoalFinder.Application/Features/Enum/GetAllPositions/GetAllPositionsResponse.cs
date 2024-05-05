using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Enum.GetAllPositions;

/// <summary>
///     Response for get all positions features.
/// </summary>
public sealed class GetAllPositionsResponse : IFeatureResponse
{
    public GetAllPositionsResponseStatusCode StatusCode { get; init; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {

        public IEnumerable<Position> Positions { get; set; }

        public sealed class Position
        {
            public Guid PositionId { get; init; }

            public string Name { get; init; }
        }
    }
}
