using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Match.GetMatchesUpcoming;

/// <summary>
///     Handler for get matches upcoming features.
/// </summary>
internal sealed class GetMatchesUpcomingHandler
    : IFeatureHandler<GetMatchesUpcomingRequest, GetMatchesUpcomingResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMatchesUpcomingHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetMatchesUpcomingResponse> ExecuteAsync(
        GetMatchesUpcomingRequest command,
        CancellationToken ct
    )
    {
        var userId = command.GetUserId();

        var matchesUpcoming =
            await _unitOfWork.GetMatchesUpcomingRepository.GetMatchesUpComingByUserIdQuery(
                userId: userId,
                cancellationToken: ct
            );

        return new()
        {
            StatusCode = GetMatchesUpcomingResponseStatusCode.OPERATION_SUCCESS,
            Body = new()
            {
                MatchesUpcoming = matchesUpcoming.Select(
                    matchUpcoming => new GetMatchesUpcomingResponse.ResponseBody.Match
                    {
                        Description = matchUpcoming.Description,
                        StartTime = matchUpcoming.StartTime,
                        CurrentPlayers = matchUpcoming.MatchPlayers.Count(),
                        MaxMatchPlayersNeed = matchUpcoming.MaxMatchPlayersNeed,
                    }
                )
            }
        };
    }
}
