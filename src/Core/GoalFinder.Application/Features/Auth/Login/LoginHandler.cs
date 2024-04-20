using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Login request handler.
/// </summary>
internal sealed class LoginHandler : IFeatureHandler<LoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    //private readonly IRefreshTokenHandler _refreshTokenHandler;
    //private readonly IAccessTokenHandler _accessTokenHandler;

    public LoginHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
        //IRefreshTokenHandler refreshTokenHandler,
        //IAccessTokenHandler accessTokenHandler)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        //_refreshTokenHandler = refreshTokenHandler;
        //_accessTokenHandler = accessTokenHandler;
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
    public async Task<LoginResponse> ExecuteAsync(
        LoginRequest command,
        CancellationToken ct)
    {
        // Find user by username.
        var foundUser = await _userManager.FindByNameAsync(userName: command.Username);

        // User with username does not exist.
        if (Equals(objA: foundUser, objB: default))
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        // Does user have the current password.
        var doesUserHaveCurrentPassword = await _userManager.CheckPasswordAsync(
            user: foundUser,
            password: command.Password);

        // Password does not belong to user.
        if (!doesUserHaveCurrentPassword)
        {
            // Is user locked out.
            var userLockedOutResult = await _signInManager.CheckPasswordSignInAsync(
                user: foundUser,
                password: command.Password,
                lockoutOnFailure: true);

            // User is temporary locked out.
            if (userLockedOutResult.IsLockedOut)
            {
                return new()
                {
                    StatusCode = LoginResponseStatusCode.USER_IS_LOCKED_OUT
                };
            }

            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT
            };
        }

        // Has user confirmed account creation email.
        var hasUserConfirmed = await _userManager.IsEmailConfirmedAsync(user: foundUser);

        // User has not confirmed account creation email.
        if (!hasUserConfirmed)
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_EMAIL_IS_NOT_CONFIRMED
            };
        }

        // Is user not temporarily removed.
        var isUserNotTemporarilyRemoved = await _unitOfWork.UserDetailRepository.IsUserTemporarilyRemovedQueryAsync(
            userId: foundUser.Id,
            cancellationToken: ct);

        // User is temporarily removed.
        if (!isUserNotTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Get found user roles.
        var foundUserRoles = await _userManager.GetRolesAsync(user: foundUser);

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti,
                value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub,
                value: foundUser.Id.ToString()),
            new(type: "role",
                value: foundUserRoles[default])
        ];

        return new()
        {
            StatusCode = LoginResponseStatusCode.OPERATION_SUCCESS
        };

        //// Create new refresh token.
        //RefreshTokenForNewRecordBuilder builder = new();

        //var newRefreshToken = builder
        //    .WithAccessTokenId(accessTokenId: Guid.Parse(input: userClaims
        //        .First(predicate: claim => claim.Type.Equals(
        //            value: JwtRegisteredClaimNames.Jti))
        //        .Value))
        //    .WithExpiredDate(refreshTokenExpiredDate: request.RememberMe ?
        //        DateTime.UtcNow.AddDays(value: 7) :
        //        DateTime.UtcNow.AddDays(value: 3))
        //    .WithCreatedAt(refreshTokenCreatedAt: DateTime.UtcNow)
        //    .WithRefreshTokenValue(refreshTokenValue: _refreshTokenHandler.Generate(length: 15))
        //    .Complete();

        //// Add new refresh token to the database.
        //var dbResult = await CreateReFreshTokenCommandAsync(
        //    newRefreshToken: newRefreshToken,
        //    cancellationToken: ct);

        //// Cannot add new refresh token to the database.
        //if (!dbResult)
        //{
        //    return new()
        //    {
        //        StatusCode = LoginResponseStatusCode.DATABASE_OPERATION_FAIL
        //    };
        //}

        //// Generate access token.
        //var newAccessToken = _accessTokenHandler.GenerateSigningToken(claims: userClaims);

        //var userAvatarUrl = await GetUserAvatarUrlQueryAsync(
        //    userId: foundUser.Id,
        //    cancellationToken: ct);

        //return new()
        //{
        //    StatusCode = LoginResponseStatusCode.OPERATION_SUCCESS,
        //    ResponseBody = new()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken.RefreshTokenValue,
        //        User = new()
        //        {
        //            Email = foundUser.Email,
        //            AvatarUrl = userAvatarUrl
        //        }
        //    }
        //};
    }

    #region Queries
    /// <summary>
    ///     Get user avatar url query.
    /// </summary>
    /// <param name="userId">
    ///     User id.
    /// </param>
    /// <param name="cancellationToken">
    ///     Token to cancel the operation.
    /// </param>
    /// <returns>
    ///     User avatar url if found. Otherwise, empty.
    /// </returns>
    //private async Task<string> GetUserAvatarUrlQueryAsync(
    //    Guid userId,
    //    CancellationToken cancellationToken)
    //{
    //    var foundUserDetail = await _unitOfWork.UserDetailRepository.FindBySpecificationsAsync(
    //        specifications:
    //        [
    //            _superSpecificationManager.UserDetail.UserDetailByIdSpecification(
    //                userId: userId),
    //            _superSpecificationManager.UserDetail.SelectFieldsFromUserDetailSpecification.Ver1()
    //        ],
    //        cancellationToken: cancellationToken);

    //    return foundUserDetail.AvatarUrl ?? string.Empty;
    //}
    #endregion

    #region Commands
    /// <summary>
    ///     Creates a new refresh token in the database.
    /// </summary>
    /// <param name="newRefreshToken">
    ///     The new refresh token.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system.
    /// </param>
    /// <returns>
    ///     True if the new refresh token is
    ///     created successfully. False otherwise.
    /// </returns>
    private async Task<bool> CreateReFreshTokenCommandAsync(
        RefreshToken newRefreshToken,
        CancellationToken cancellationToken)
    {
        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.RefreshTokenRepository.AddAsync(
                        newEntity: newRefreshToken,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken: cancellationToken);
                }
                finally
                {
                    await _unitOfWork.DisposeTransactionAsync();
                }
            });

        return executedTransactionResult;
    }
    #endregion
}
