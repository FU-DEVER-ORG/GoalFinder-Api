using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Application.Shared.Tokens.AccessToken;
using GoalFinder.Application.Shared.Tokens.RefreshToken;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Handler for RefreshAccessToken
/// </summary>

internal class RefreshAccessTokenHandler
    : IFeatureHandler<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;
    private readonly UserManager<Data.Entities.User> _userManager;

    public RefreshAccessTokenHandler(
        IUnitOfWork unitOfWork,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler,
        UserManager<Data.Entities.User> userManager
    )
    {
        _unitOfWork = unitOfWork;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
        _userManager = userManager;
    }

    /// <summary>
    ///     Execute for RefreshAccessToken
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

    public async Task<RefreshAccessTokenResponse> ExecuteAsync(
        RefreshAccessTokenRequest command,
        CancellationToken ct
    )
    {
        //Find refresh token is existed or not
        var foundRefreshToken =
            await _unitOfWork.RefreshAccessTokenRepository.FindByRefreshTokenValueQueryAsync(
                command.RefreshToken,
                ct
            );
        //If refresh token is not existed
        if (Equals(objA: foundRefreshToken, objB: default(Data.Entities.RefreshToken)))
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND
            };
        }

        //Checking expired or not
        if (foundRefreshToken.ExpiredDate < DateTime.UtcNow)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_EXPIRED
            };
        }
        //founder user
        var foundUser = await _userManager.FindByIdAsync(foundRefreshToken.UserId.ToString());
        // Is user not temporarily removed
        var isUserTemporarilyRemoved =
            await _unitOfWork.RefreshAccessTokenRepository.IsUserTemporarilyRemovedQueryAsync(
                foundUser.Id,
                ct
            );
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }
        //Initialize new list of user claims
        var foundUserRoles = await _userManager.GetRolesAsync(user: foundUser);

        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub, value: foundUser.Id.ToString()),
            new(type: "role", value: foundUserRoles[default])
        ];

        // Create new refresh token
        var isRemember =
            foundRefreshToken.ExpiredDate - foundRefreshToken.CreatedAt
            == TimeSpan.FromDays(value: 7);

        RefreshToken newRefreshToken =
            new()
            {
                AccessTokenId = Guid.Parse(
                    input: userClaims
                        .First(predicate: claim =>
                            claim.Type.Equals(value: JwtRegisteredClaimNames.Jti)
                        )
                        .Value
                ),
                UserId = foundUser.Id,
                ExpiredDate = isRemember
                    ? DateTime.UtcNow.AddDays(value: 7)
                    : DateTime.UtcNow.AddDays(value: 3),
                CreatedAt = DateTime.UtcNow,
                RefreshTokenValue = _refreshTokenHandler.Generate(length: 15)
            };
        // Save new refresh token
        var dbResult =
            await _unitOfWork.RefreshAccessTokenRepository.CreateRefreshTokenCommandAsync(
                refreshToken: newRefreshToken,
                cancellationToken: ct
            );
        //  If database operation is failed
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }
        // Remove old refresh token
        var removeRefreshTokenResult =
            await _unitOfWork.RefreshAccessTokenRepository.DeleteRefreshTokenCommandAsync(
                accessTokenId: foundRefreshToken.AccessTokenId,
                cancellationToken: ct
            );
        // If database operation is failed
        if (!removeRefreshTokenResult)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.DATABASE_OPERATION_FAILED
            };
        }
        var newAccesstoken = _accessTokenHandler.GenerateSigningToken(claims: userClaims);
        // Return response
        return new()
        {
            StatusCode = RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccesstoken,
                RefreshToken = newRefreshToken.RefreshTokenValue
            }
        };
    }
}
