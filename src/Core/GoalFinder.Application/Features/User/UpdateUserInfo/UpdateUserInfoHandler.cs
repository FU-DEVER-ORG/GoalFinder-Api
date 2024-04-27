using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Update user request handler.
/// </summary>
internal sealed class UpdateUserInfoHandler : IFeatureHandler<
    UpdateUserInfoRequest,
    UpdateUserInfoResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserInfoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateUserInfoResponse> ExecuteAsync(
        UpdateUserInfoRequest command,
        CancellationToken ct)
    {
        command.PositionIds = command.PositionIds.Distinct();

        #region Validations
        // Is experience name found.
        var isExperienceFound = await _unitOfWork.UpdateUserInfoRepository
            .IsExperienceFoundByIdQueryAsync(
                experienceId: command.ExperienceId,
                cancellationToken: ct);

        if (!isExperienceFound)
        {
            return new()
            {
                StatusCode = UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        // Is competition level name found.
        var isCompetitionLevelFound = await _unitOfWork.UpdateUserInfoRepository
            .IsCompetitionLevelFoundByIdQueryAsync(
                competitionLevelId: command.CompetitionLevelId,
                cancellationToken: ct);

        if (!isCompetitionLevelFound)
        {
            return new()
            {
                StatusCode = UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        // Are position names found.
        var arePositionsFound = await _unitOfWork.UpdateUserInfoRepository
            .ArePositionsFoundByIdsQueryAsync(
                positionIds: command.PositionIds,
                cancellationToken: ct);

        if (!arePositionsFound)
        {
            return new()
            {
                StatusCode = UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        #endregion
        // Is user temporarily removed.
        var isUserTemporarilyRemoved = await _unitOfWork.UpdateUserInfoRepository
            .IsUserTemporarilyRemovedQueryAsync(
                userId: command.GetUserId(),
                cancellationToken: ct);

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdateUserInfoResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Is username already taken by other user.
        var isUsernameAlreadyTaken = await _unitOfWork.UpdateUserInfoRepository
            .IsUserNameAlreadyTakenQueryAsync(
                currentUserId: command.GetUserId(),
                userName: command.UserName,
                cancellationToken: ct);

        //Username has been already taken by other user.
        if (isUsernameAlreadyTaken)
        {
            return new()
            {
                StatusCode = UpdateUserInfoResponseStatusCode.USERNAME_IS_ALREADY_TAKEN
            };
        }

        var foundUser = await _unitOfWork.UpdateUserInfoRepository
            .GetUserDetailsQueryAsync(
                userId: command.GetUserId(),
                cancellationToken: ct);

        var dbResult = await _unitOfWork.UpdateUserInfoRepository
            .UpdateUserCommandAsync(
                updateUser: InitNewUpdateUser(command: command),
                currentUser: foundUser,
                currentPositionIds: foundUser.UserPositions.Select(
                    selector: userPosition => userPosition.PositionId),
                newPositionIds: command.PositionIds,
                cancellationToken: ct);

        //Cannot update user information
        if (!dbResult)
        {
            return new()
            {
                StatusCode = UpdateUserInfoResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateUserInfoResponseStatusCode.UPDATE_SUCCESS
        };
    }

    private UserDetail InitNewUpdateUser(UpdateUserInfoRequest command)
    {
        UserDetail newUpdateUser = new()
        {
            UserId = command.GetUserId(),
            User = new()
        };

        newUpdateUser.User.UserName = command.UserName;
        newUpdateUser.FirstName = command.FirstName;
        newUpdateUser.LastName = command.LastName;
        newUpdateUser.Description = command.Description;
        newUpdateUser.Address = command.Address;
        newUpdateUser.AvatarUrl = command.AvatarUrl;
        newUpdateUser.BackgroundUrl = command.BackgroundUrl;
        newUpdateUser.ExperienceId = command.ExperienceId;
        newUpdateUser.CompetitionLevelId = command.CompetitionLevelId;
        newUpdateUser.UpdatedAt = DateTime.UtcNow;
        newUpdateUser.UpdatedBy = command.GetUserId();

        return newUpdateUser;
    }
}
