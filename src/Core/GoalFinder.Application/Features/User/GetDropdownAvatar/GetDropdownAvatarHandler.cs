using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.User.GetDropdownAvatar;

/// <summary>
///     Handler for get user dropdown avatar features.
/// </summary>
internal sealed class GetDropdownAvatarHandler
    : IFeatureHandler<GetDropdownAvatarRequest, GetDropdownAvatarResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDropdownAvatarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDropdownAvatarResponse> ExecuteAsync(
        GetDropdownAvatarRequest command,
        CancellationToken ct
    )
    {
        // Find User By user id
        var isUserFound =
            await _unitOfWork.GetDropdownAvatarRepository.IsUserFoundByUserIdQueryAsync(
                userId: command.GetUserId(),
                cancellationToken: ct
            );

        //Validate User
        if (!isUserFound)
        {
            return new() { StatusCode = GetDropdownAvatarResponseStatusCode.USER_IS_NOT_FOUND };
        }

        //Get user detail.
        var userDetail = await _unitOfWork.GetDropdownAvatarRepository.GetUserDetailByUserIdAsync(
            userId: command.GetUserId(),
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetDropdownAvatarResponseStatusCode.OPERATION_SUCCESS,
            Body = new()
            {
                UserName = userDetail.User.UserName,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName
            }
        };
    }
}
