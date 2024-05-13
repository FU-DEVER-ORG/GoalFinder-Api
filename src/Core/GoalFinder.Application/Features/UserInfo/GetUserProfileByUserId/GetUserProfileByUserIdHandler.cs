using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

/// <summary>
/// Get User Profile By User Id
/// </summary>

internal class GetUserProfileByUserIdHandler
    : IFeatureHandler<GetUserProfileByUserIdRequest, GetUserProfileByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserProfileByUserIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserProfileByUserIdResponse> ExecuteAsync(
        GetUserProfileByUserIdRequest command,
        CancellationToken ct
    )
    {
        //Find User By username
        var isUserFound =
            await _unitOfWork.GetUserProfileByUserIdRepository.IsUserFoundByUserIdQueryAsync(
                userId: command.Id,
                cancellationToken: ct
            );

        //Validate User
        if (!isUserFound)
        {
            return new()
            {
                StatusCode = GetUserProfileByUserIdResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.GetUserProfileByUserIdRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: command.Id,
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = GetUserProfileByUserIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        //Get user detail.
        var userDetail = await _unitOfWork.GetUserProfileByUserIdRepository.GetUserDetailAsync(
            userId: command.Id,
            cancellationToken: ct
        );

        //Get matches of user
        var matches = await _unitOfWork.GetUserProfileByUserIdRepository.GetFootballMatchByIdAsync(
            userId: command.Id,
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetUserProfileByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                UserDetail = new GetUserProfileByUserIdResponse.Body.User
                {
                    NickName = userDetail.NickName,
                    LastName = userDetail.LastName,
                    FirstName = userDetail.FirstName,
                    Description = userDetail.Description,
                    PrestigeScore = userDetail.PrestigeScore,
                    Address = userDetail.Address,
                    BackgroundUrl = userDetail.BackgroundUrl,
                    AvatarUrl = userDetail.AvatarUrl,
                    Experience = userDetail.Experience.FullName,
                    CompetitionLevel = userDetail.CompetitionLevel.FullName,
                    Positions = userDetail.UserPositions.Select(userPosition =>
                        userPosition?.Position?.FullName
                    )
                },
                FootballMatches = matches.Select(
                    selector: match => new GetUserProfileByUserIdResponse.Body.FootballMatch
                    {
                        Id = match.Id,
                        PitchAddress = match.PitchAddress,
                        MaxMatchPlayersNeed = match.MaxMatchPlayersNeed,
                        PitchPrice = match.PitchPrice,
                        Description = match.Description,
                        StartTime = match.StartTime.ToString(),
                        Address = match.Address,
                        CompetitionLevel = match.CompetitionLevel.FullName
                    }
                )
            }
        };
    }
}
