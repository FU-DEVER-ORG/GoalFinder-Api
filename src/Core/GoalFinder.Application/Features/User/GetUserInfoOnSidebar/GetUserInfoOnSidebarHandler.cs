using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

/// <summary>
///     Handler for get user info on sidebar features.
/// </summary>
internal sealed class GetUserInfoOnSidebarHandler
    : IFeatureHandler<GetUserInfoOnSidebarRequest, GetUserInfoOnSidebarResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserInfoOnSidebarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserInfoOnSidebarResponse> ExecuteAsync(
        GetUserInfoOnSidebarRequest command,
        CancellationToken ct
    )
    {
        //Find User By user id
        var isUserFound =
            await _unitOfWork.GetUserInfoOnSidebarRepository.IsUserFoundByUserIdQueryAsync(
                userId: command.GetUserId(),
                cancellationToken: ct
            );

        //Validate User
        if (!isUserFound)
        {
            return new() { StatusCode = GetUserInfoOnSidebarResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.GetUserInfoOnSidebarRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: command.GetUserId(),
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = GetUserInfoOnSidebarResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        //Get user detail.
        var userDetail = await _unitOfWork.GetUserInfoOnSidebarRepository.GetUserDetailAsync(
            userId: command.GetUserId(),
            cancellationToken: ct
        );

        var addressTokens = userDetail.Address.Split(separator: "<token>");

        return new()
        {
            StatusCode = GetUserInfoOnSidebarResponseStatusCode.OPERATION_SUCCESS,
            Body = new()
            {
                UserName = userDetail.User.UserName,
                PrestigeScore = userDetail.PrestigeScore,
                Area = addressTokens.Equals(obj: Array.Empty<string>())
                    ? string.Empty
                    : addressTokens[0],
            }
        };
    }
}
