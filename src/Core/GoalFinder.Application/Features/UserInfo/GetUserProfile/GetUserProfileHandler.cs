using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;
/// <summary>
///     Get User Profile Handler
/// </summary>
internal sealed class GetUserProfileHandler : IFeatureHandler<
    GetUserProfileRequest,
    GetUserProfileResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserProfileHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
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
        CancellationToken ct)
    {
        //Find User By username
        // var foundUser = await _userManager.FindByNameAsync(userName: command.UserName);

        //Validate User
        // if (Equals(objA: foundUser, objB: default))
        // {
        //     return new()
        //     {
        //         StatusCode = GetUserProfileResponseStatusCode.USER_IS_NOT_FOUND
        //     };
        // }


        // Is user not temporarily removed.
        // var isUserTemporarilyRemoved = await _unitOfWork.LoginRepository
        //     .IsUserTemporarilyRemovedQueryAsync(
        //         userId: foundUser.Id,
        //         cancellationToken: ct);

        // User is temporarily removed.
        // if (isUserTemporarilyRemoved)
        // {
        //     return new()
        //     {
        //         StatusCode = GetUserProfileResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
        //     };
        // }

        //Get UserDetail
        // var userDetail = await _unitOfWork.GetUserProfileRepository
        //     .GetUserDetailAsync(userId: foundUser.Id, cancellationToken: ct);

        var userDetail = await _unitOfWork.GetUserProfileFakeDb.GetUserDetailAsync(userId: Guid.NewGuid(), cancellationToken: ct);

        var userDetail1 = userDetail.UserPositions.Select(x => x?.Position?.FullName);
        //Get matches of user
        // var matches = await _unitOfWork.GetUserProfileRepository
        //     .GetFootballMatchByIdAsync(userId: foundUser.Id, cancellationToken: ct);

        var matches = await _unitOfWork.GetUserProfileFakeDb.GetFootballMatchByIdAsync(userId: Guid.NewGuid(), cancellationToken: ct);


        var footballMatchesResponse = matches.Select(match => new GetUserProfileResponse.Body.FootballMatch
        {
            Id = match.Id,
            PitchAddress = match?.PitchAddress,
            MaxMatchPlayersNeed = match.MaxMatchPlayersNeed,
            PitchPrice = match.PitchPrice,
            Description = match.Description,
            StartTime = match.StartTime.ToString(),
            Address = match.Address,
            CompetitionLevel = match?.CompetitionLevel?.FullName
        });
        
        return new()
        {
            StatusCode = GetUserProfileResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                UserDetail = new()
                {
                    LastName = userDetail.LastName,

                    FirstName = userDetail.FirstName,

                    Description = userDetail.Description,

                    PrestigeScore = userDetail.PrestigeScore,

                    Address = userDetail.Address,

                    AvatarUrl = userDetail.AvatarUrl,

                    Experience = userDetail.Experience.FullName,

                    CompetitionLevel = userDetail.CompetitionLevel.FullName,

                    Positions = userDetail1
                },
                FootballMatches = footballMatchesResponse
            }

        };
    }
}



