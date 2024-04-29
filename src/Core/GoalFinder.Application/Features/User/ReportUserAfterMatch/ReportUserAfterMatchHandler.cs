using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
///      Report user after match handler
/// </summary>

internal sealed class ReportUserAfterMatchHandler
    : IFeatureHandler<ReportUserAfterMatchRequest, ReportUserAfterMatchResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportUserAfterMatchHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReportUserAfterMatchResponse> ExecuteAsync(
        ReportUserAfterMatchRequest command,
        CancellationToken ct
    )
    {
        // Is user found by id
        var isUserFound =
            await _unitOfWork.ReportUserAfterMatchRepository.IsUserFoundByIdQueryAsync(
                playerId: command.GetPlayerId(),
                cancellationToken: ct
            );

        if (isUserFound)
        {
            return new() { StatusCode = ReportUserAfterMatchResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.ReportUserAfterMatchRepository.IsUserTemporarilyRemovedQueryAsync(
                playerId: command.GetPlayerId(),
                cancellationToken: ct
            );

        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ReportUserAfterMatchResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Is football match found by id
        var IsFootballMatchFoundById =
            await _unitOfWork.ReportUserAfterMatchRepository.IsFootballMatchFoundByIdQueryAsync(
                footballMatchId: command.GetFootballMatchId(),
                cancellationToken: ct
            );

        if (!IsFootballMatchFoundById)
        {
            return new()
            {
                StatusCode = ReportUserAfterMatchResponseStatusCode.FOOTBALL_MATCH_IS_NOT_FOUND
            };
        }

        // Is player exist in football match
        var IsPlayerExistInFootballMatch =
            await _unitOfWork.ReportUserAfterMatchRepository.IsPlayerExistInFootballMatchQueryAsync(
                footballMatchId: command.GetFootballMatchId(),
                playerId: command.GetPlayerId(),
                cancellationToken: ct
            );

        if (!IsPlayerExistInFootballMatch)
        {
            return new()
            {
                StatusCode =
                    ReportUserAfterMatchResponseStatusCode.PLAYER_DOES_NOT_EXIST_IN_FOOTBALL_MATCH
            };
        }

        var IsEndTimeFootballMatchDone =
            await _unitOfWork.ReportUserAfterMatchRepository.IsEndTimeFootballMatchDoneQueryAsync(
                footballMatchId: command.GetFootballMatchId(),
                currentTime: command.currentTime,
                cancellationToken: ct
            );

        if (IsEndTimeFootballMatchDone)
        {
            return new()
            {
                StatusCode =
                    ReportUserAfterMatchResponseStatusCode.FOOTBALL_MATCH_ENDTIME_IS_STILL_AVAILABLE
            };
        }

        var dbResult =
            await _unitOfWork.ReportUserAfterMatchRepository.ReportUserAfterMatchCommandAsync(
                bonusAfterMatch: command.PrestigeScore,
                playerId: command.GetPlayerId(),
                cancellationToken: ct
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode = ReportUserAfterMatchResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new() { StatusCode = ReportUserAfterMatchResponseStatusCode.OPERATION_SUCCESS };
    }
}
