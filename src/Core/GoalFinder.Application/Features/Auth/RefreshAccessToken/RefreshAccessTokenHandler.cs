using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Application.Shared.Tokens.AccessToken;
using GoalFinder.Application.Shared.Tokens.RefreshToken;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Handler for RefreshAccessToken
/// </summary>

internal sealed class RefreshAccessTokenHandler
    : IFeatureHandler<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RefreshAccessTokenHandler(
        IUnitOfWork unitOfWork,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
        _httpContextAccessor = httpContextAccessor;
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
                refreshTokenValue: command.RefreshToken,
                cancellationToken: ct
            );

        //If refresh token is not existed
        if (Equals(objA: foundRefreshToken, objB: default(RefreshToken)))
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND
            };
        }

        // Checking expired or not
        if (foundRefreshToken.ExpiredDate < DateTime.UtcNow)
        {
            return new() { StatusCode = RefreshAccessTokenResponseStatusCode.REQUIRE_LOGIN_AGAIN };
        }

        // New access token id.
        var newAccessTokenId = Guid.NewGuid();

        // New refresh token value.
        var newRefreshTokenValue = _refreshTokenHandler.Generate(length: 15);

        // Update refresh token
        var dbResult =
            await _unitOfWork.RefreshAccessTokenRepository.UpdateRefreshTokenCommandAsync(
                accessTokenId: newAccessTokenId,
                refreshTokenValue: newRefreshTokenValue,
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

        // Return response
        return new()
        {
            StatusCode = RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = _accessTokenHandler.GenerateSigningToken(
                    claims:
                    [
                        new(JwtRegisteredClaimNames.Jti, newAccessTokenId.ToString()),
                        new(JwtRegisteredClaimNames.Sub, foundRefreshToken.UserId.ToString()),
                        new("role", _httpContextAccessor.HttpContext.User.FindFirstValue("role"))
                    ]
                ),
                RefreshToken = newRefreshTokenValue
            }
        };
    }
}
