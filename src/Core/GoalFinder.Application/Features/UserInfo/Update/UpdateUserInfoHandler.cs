using GoalFinder.Application.Shared.Commons;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.UserInfo.Update
{
    /// <summary>
    ///     Update user request handler.
    /// </summary>
    internal sealed class UpdateUserInfoHandler : IFeatureHandler<UpdateUserInfoRequest, UpdateUserInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserDetail> _userManager;
        public UpdateUserInfoHandler(IUnitOfWork unitOfWork, UserManager<UserDetail> userManager) 
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<UpdateUserInfoResponse> ExecuteAsync(UpdateUserInfoRequest command, CancellationToken ct) 
        {

            var isUserNameExisted = await _unitOfWork.UpdateUserInfoRepository
                .IsUserFoundByUserNameQueryAsync(userName: command.UserName, cancellationToken: ct);
            //Username has been existed
            if (isUserNameExisted) 
            { 
                return new() 
                {
                    StatusCode = UpdateUserInfoResponseStatusCode.USERNAME_IS_EXISTED 
                };
            }

            //Find user need to update information
            var updateUser = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (Equals(objA: updateUser, objB: default))
            {
                return new()
                {
                    StatusCode = UpdateUserInfoResponseStatusCode.USER_NOT_FOUND
                };
            }

            //Update information for user
            FinishUpdateUser(updateUser: updateUser, command: command);



            var dbResult = await _unitOfWork.UpdateUserInfoRepository
                .UpdateUserCommandAsync(
                    updateUser: updateUser,
                    userManager: _userManager,
                    cancellationToken: ct
                );

            //Cannot update user information
            if(!dbResult)
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

        /// <summary>
        ///     Update the user information
        /// </summary>
        /// <param name="updateUser">
        ///     The updated user.
        /// </param>
        /// 
        private void FinishUpdateUser(UserDetail updateUser, UpdateUserInfoRequest command) 
        {
            updateUser.User.UserName = command.UserName;
            updateUser.FirstName = command.FirstName;
            updateUser.LastName = command.LastName;
            updateUser.Description = command.Description;
            updateUser.AvatarUrl = command.AvatarUrl;
            updateUser.Experience.FullName = command.Experience;
            updateUser.CompetitionLevel.FullName = command.CompetitionLevel;
            updateUser.UserPositions = command.Position;
            updateUser.UpdatedAt = DateTime.MinValue;
            updateUser.UpdatedBy = command.UserId;

        }


    }
}
