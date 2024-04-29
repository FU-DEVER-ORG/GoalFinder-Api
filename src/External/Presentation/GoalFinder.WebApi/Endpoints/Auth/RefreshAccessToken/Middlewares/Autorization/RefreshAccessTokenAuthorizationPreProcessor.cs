using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Common;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Middlewares.Autorization;

/// <summary>
///     Pre processor for refresh access token
/// </summary>
internal sealed class RefreshAccessTokenAuthorizationPreProcessor
    : PreProcessor<RefreshAccessTokenRequest, RefreshAccessTokenStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public RefreshAccessTokenAuthorizationPreProcessor(
        IServiceScopeFactory serviceScopeFactory,
        TokenValidationParameters tokenValidationParameters
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<RefreshAccessTokenRequest> context,
        RefreshAccessTokenStateBag state,
        CancellationToken ct
    )
    {
        JsonWebTokenHandler jsonWebTokenHandler = new();

        // validate token
        var validateTokenResult = await jsonWebTokenHandler.ValidateTokenAsync(
            token: context.HttpContext.Request.Headers.Authorization[0].Split(separator: " ")[1],
            validationParameters: _tokenValidationParameters
        );
        // if token is invalid send response
        if (!validateTokenResult.IsValid)
        {
            await SendResponseAsync(
                statusCode: RefreshAccessTokenResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }
        // get jti claim
        var jtiClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Jti
        );

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var userManager = scope.ServiceProvider.GetRequiredService<
            UserManager<Data.Entities.User>
        >();

        // check if refresh token is found
        var isRefreshTokenFound =
            await unitOfWork.RefreshAccessTokenRepository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(input: jtiClaim),
                cancellationToken: ct
            );
        // if not found send response
        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(
                statusCode: RefreshAccessTokenResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }
        // get sub claim
        var subClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );
        // find user by id
        var foundUser = await userManager.FindByIdAsync(
            userId: Guid.Parse(input: subClaim).ToString()
        );
        if (Equals(objA: foundUser, objB: default))
        {
            await SendResponseAsync(
                statusCode: RefreshAccessTokenResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }

        var isUserTempopraryRemoved =
            await unitOfWork.RefreshAccessTokenRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct
            );
        if (isUserTempopraryRemoved)
        {
            await SendResponseAsync(
                statusCode: RefreshAccessTokenResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }

        // Get the role claim from the access token.
        var roleClaim = context.HttpContext.User.FindFirstValue(claimType: "role");

        // Is user in role.
        var isUserInRole = await userManager.IsInRoleAsync(user: foundUser, role: roleClaim);

        // User is not in role.
        if (!isUserInRole)
        {
            await SendResponseAsync(
                statusCode: RefreshAccessTokenResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }
    }

    /// <summary>
    /// Send response async for given status code
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="context"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private static Task SendResponseAsync(
        RefreshAccessTokenResponseStatusCode statusCode,
        IPreProcessorContext<RefreshAccessTokenRequest> context,
        CancellationToken ct
    )
    {
        var httpResponse = LazyRefreshAccessTokenHttpResponseMapper
            .Get()
            .Resolve(statusCode: statusCode)
            .Invoke(arg1: context.Request, arg2: new() { StatusCode = statusCode });

        context.HttpContext.MarkResponseStart();

        return context.HttpContext.Response.SendAsync(
            response: httpResponse,
            statusCode: httpResponse.HttpCode,
            cancellation: ct
        );
    }
}
