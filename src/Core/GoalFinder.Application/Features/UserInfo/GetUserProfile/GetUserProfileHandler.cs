using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;

/// <summary>
///     Get User Profile Handler
/// </summary>
internal sealed class GetUserProfileHandler
    : IFeatureHandler<GetUserProfileRequest, GetUserProfileResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserProfileHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetUserProfileResponse> ExecuteAsync(
        GetUserProfileRequest command,
        CancellationToken ct
    )
    {
        //Find User By username
        var foundUser = await _unitOfWork.GetUserProfileRepository.GetUserByNickNameQueryAsync(
            nickName: command.NickName,
            cancellationToken: ct
        );

        //Validate User
        if (Equals(objA: foundUser, objB: default))
        {
            return new() { StatusCode = GetUserProfileResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.GetUserProfileRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.UserId,
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = GetUserProfileResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        //Get user detail.
        var userDetail = await _unitOfWork.GetUserProfileRepository.GetUserDetailAsync(
            userId: foundUser.UserId,
            cancellationToken: ct
        );

        //Get matches of user
        var matches = await _unitOfWork.GetUserProfileRepository.GetFootballMatchByIdAsync(
            userId: foundUser.UserId,
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetUserProfileResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                UserDetail = new GetUserProfileResponse.Body.User
                {
                    NickName = userDetail.NickName,
                    LastName = userDetail.LastName,
                    FirstName = userDetail.FirstName,
                    Description = userDetail.Description,
                    PrestigeScore = userDetail.PrestigeScore,
                    Address = userDetail.Address,
                    AvatarUrl = userDetail.AvatarUrl,
                    Experience = userDetail.Experience.FullName,
                    CompetitionLevel = userDetail.CompetitionLevel.FullName,
                    Positions = userDetail.UserPositions.Select(userPosition =>
                        userPosition?.Position?.FullName
                    )
                },
                FootballMatches = matches.Select(
                    selector: match => new GetUserProfileResponse.Body.FootballMatch
                    {
                        Id = match.Id,
                        PitchAddress = match.PitchAddress,
                        MaxMatchPlayersNeed = match.MaxMatchPlayersNeed,
                        PitchPrice = match.PitchPrice,
                        Description = match.Description,
                        StartTime = match.StartTime.ToString(),
                        Address = match.Address,
                        CompetitionLevel = match.CompetitionLevel?.FullName
                    }
                )
            }
        };
    }
}
