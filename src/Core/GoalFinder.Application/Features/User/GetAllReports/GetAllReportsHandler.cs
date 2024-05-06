using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.Extensions.Primitives;
using static GoalFinder.Data.Entities.FootballMatch.MetaData;

namespace GoalFinder.Application.Features.User.GetAllReports;

/// <summary>
///     Get All Reports Handler
/// </summary>
internal sealed class GetAllReportsHandler
    : IFeatureHandler<GetAllReportsRequest, GetAllReportsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllReportsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="command">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<GetAllReportsResponse> ExecuteAsync(
        GetAllReportsRequest command,
        CancellationToken ct
    )
    {
        var foundMatch = await _unitOfWork.GetAllReportsRepository.GetFootballMatchByIdQueryAsync(
            matchId: command.MatchId,
            cancellationToken: ct
        );

        if (Equals(objA: foundMatch, objB: default))
        {
            return new() { StatusCode = GetAllReportsStatusCode.MATCH_ID_NOT_FOUND };
        }

        //Get match players
        var matchPlayers = await _unitOfWork.GetAllReportsRepository.GetMatchPlayerByMatchIdAndUserIdAsync(
            matchId: command.MatchId,
            userId: command.UserId,
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetAllReportsStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                FootballMatch = new GetAllReportsResponse.Body.Match
                {
                    PitchAddress = foundMatch.PitchAddress,
                    MaxMatchPlayersNeed = foundMatch.MaxMatchPlayersNeed,
                    PitchPrice = foundMatch.PitchPrice,
                    Description = foundMatch.Description,
                    StartTime = foundMatch.StartTime.ToString(),
                    EndTime = foundMatch.EndTime.ToString(),
                    Address = foundMatch.Address,
                    CompetitionLevel = foundMatch.CompetitionLevel?.FullName
                },

                MatchPlayers = await Task.WhenAll(
                    matchPlayers.Select(
                        async player => new GetAllReportsResponse.Body.MatchPlayerDetails
                        {
                            PlayerId = player.PlayerId,
                            NumberOfReports = player.NumberOfReports,
                        }
                    )
                )
            }
        };
    }
}
