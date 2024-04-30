using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Match.CreateMatch;

/// <summary>
///     Create match request handler.
/// </summary>
internal sealed class CreateMatchHandler : IFeatureHandler<CreateMatchRequest, CreateMatchResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMatchHandler(IUnitOfWork unitOfWork)
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
    public async Task<CreateMatchResponse> ExecuteAsync(
        CreateMatchRequest command,
        CancellationToken ct
    )
    {
        // Is competition level name found.
        var isCompetitionLevelFound =
            await _unitOfWork.CreateMatchRepository.IsCompetitionLevelFoundByIdQueryAsync(
                competitionLevelId: command.CompetitionLevelId,
                cancellationToken: ct
            );

        if (!isCompetitionLevelFound)
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.INPUT_VALIDATION_FAIL };
        }

        // Check starting time must be later than 1:30 from now
        var isNotValidStartTime = IsNotValidStartTime(command.StartTime);

        if (isNotValidStartTime)
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.INPUT_VALIDATION_FAIL };
        }

        // Is user found
        var userDetail = await _unitOfWork.CreateMatchRepository.GetUserDetailByUserIdQueryAsync(
            userId: command.GetHostId(),
            cancellationToken: ct
        );

        if (Equals(objA: userDetail, objB: default))
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.USER_ID_IS_NOT_FOUND };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.CreateMatchRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: command.GetHostId(),
                cancellationToken: ct
            );

        if (isUserTemporarilyRemoved)
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.USER_IS_TEMPORARILY_REMOVED };
        }

        // Has the user created match in this day
        var isUserCreatedMatchToday =
            await _unitOfWork.CreateMatchRepository.IsUserCreatedMatchThisDayQueryAsync(
                userId: command.GetHostId(),
                startTime: command.StartTime,
                cancellationToken: ct
            );

        if (isUserCreatedMatchToday)
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.LIMIT_ONE_MATCH_PER_DAY };
        }

        // Check the user's prestige always less than or equal with match's min prestige
        if (userDetail.PrestigeScore < command.MinPrestigeScore)
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.PRESTIGE_IS_NOT_ENOUGH };
        }

        var footballMatch = InitNewFootballMatch(createMatchRequest: command);

        var dbResult = await _unitOfWork.CreateMatchRepository.CreateMatchCommandAsync(
            userId: command.GetHostId(),
            footballMatch: footballMatch,
            matchPlayer: InitNewMatchPlayer(matchId: footballMatch.Id, userId: command.GetHostId()),
            cancellationToken: ct
        );

        if (!dbResult)
        {
            return new() { StatusCode = CreateMatchResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        return new() { StatusCode = CreateMatchResponseStatusCode.OPERATION_SUCCESS };
    }

    private FootballMatch InitNewFootballMatch(CreateMatchRequest createMatchRequest)
    {
        FootballMatch footballMatch =
            new()
            {
                Id = Guid.NewGuid(),
                PitchAddress = createMatchRequest.PitchAddress,
                MaxMatchPlayersNeed = createMatchRequest.MaxMatchPlayersNeed,
                PitchPrice = createMatchRequest.PitchPrice,
                Description = createMatchRequest.Description,
                MinPrestigeScore = createMatchRequest.MinPrestigeScore,
                StartTime = createMatchRequest.StartTime,
                Address = createMatchRequest.Address,
                EndTime = createMatchRequest.StartTime.AddHours(1),
                UpdatedAt = DateTime.MinValue,
                UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createMatchRequest.GetHostId(),
                RemovedAt = DateTime.MinValue,
                RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                CompetitionLevelId = createMatchRequest.CompetitionLevelId,
                HostId = createMatchRequest.GetHostId(),
            };

        return footballMatch;
    }

    private MatchPlayer InitNewMatchPlayer(Guid matchId, Guid userId)
    {
        return new()
        {
            MatchId = matchId,
            PlayerId = userId,
            NumberOfReports = default,
        };
    }

    private bool IsNotValidStartTime(DateTime startTime)
    {
        return (startTime - DateTime.UtcNow).TotalMinutes < 90;
    }
}
