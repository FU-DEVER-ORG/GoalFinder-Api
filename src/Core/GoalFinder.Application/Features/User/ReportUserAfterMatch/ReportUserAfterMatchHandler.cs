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
                userId: command.UserId,
                cancellationToken: ct
            );

        if (isUserFound)
        {
            return new() { StatusCode = ReportUserAfterMatchResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.ReportUserAfterMatchRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: command.UserId,
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
                footballMatchId: command.FootballMatchId,
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
                footballMatchId: command.FootballMatchId,
                userId: command.UserId,
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
                footballMatchId: command.FootballMatchId,
                currentTime: command.CurrentTime,
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

        var IsFormWithin24Hours =
            await _unitOfWork.ReportUserAfterMatchRepository.IsFormWithin24HoursQueryAsync(
                footballMatchId: command.FootballMatchId,
                currentTime: command.CurrentTime,
                cancellationToken: ct
            );

        if (!IsFormWithin24Hours)
        {
            return new() 
            { 
                StatusCode = ReportUserAfterMatchResponseStatusCode.FORM_HAS_EXPIRED 
            };
        }

        var dbResult =
            await _unitOfWork.ReportUserAfterMatchRepository.ReportUserAfterMatchCommandAsync(
                playerScores: command.PlayerScores,
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
