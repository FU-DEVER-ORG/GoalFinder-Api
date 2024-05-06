using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetMatchDetail;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Authorization;

/// <summary>
/// Pre processor for authorization.
/// </summary>

internal sealed class GetMatchDetailAuthorizationPreProcessor
    : PreProcessor<GetMatchDetailRequest, GetMatchDetailStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public GetMatchDetailAuthorizationPreProcessor(
        IServiceScopeFactory serviceScopeFactory,
        TokenValidationParameters tokenValidationParameters
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<GetMatchDetailRequest> context,
        GetMatchDetailStateBag state,
        CancellationToken ct
    )
    {
        JsonWebTokenHandler jsonWebTokenHandler = new();

        // Validate access token.
        var validateTokenResult = await jsonWebTokenHandler.ValidateTokenAsync(
            token: context.HttpContext.Request.Headers.Authorization[0]?.Split(separator: " ")[1],
            validationParameters: _tokenValidationParameters
        );

        // Token is not valid.
        if (!validateTokenResult.IsValid)
        {
            await SendResponseAsync(
                statusCode: GetMatchDetailResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }

        // Get the jti claim from the access token.
        var jtiClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Jti
        );

        // Get access token by jti claim.
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        // Get access token by jti claim.
        var unitOfWork = scope.Resolve<IUnitOfWork>();

        // Does refresh token exist by access token id.
        var isRefreshTokenFound =
            await unitOfWork.GetMatchDetailRepository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(input: jtiClaim),
                cancellationToken: ct
            );
        // Refresh token not found.
        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(
                statusCode: GetMatchDetailResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }

        // user manager for User Entity
        var userManager = scope.Resolve<UserManager<Data.Entities.User>>();

        // Get user by subject claim from access token.
        var subjectClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get user by subject claim.
        var founduser = await userManager.FindByIdAsync(
            userId: Guid.Parse(input: subjectClaim).ToString()
        );

        // User not found.
        if (Equals(objA: founduser, objB: default(Data.Entities.User)))
        {
            await SendResponseAsync(
                statusCode: GetMatchDetailResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }

        // Set user id to request.
        context.Request.SetUserId(userId: founduser.Id);

        // Check if user is temporarily removed.
        var isUserTemporarylyRemoved =
            await unitOfWork.GetMatchDetailRepository.IsUserTemporarilyRemovedQueryAsync(
                founduser.Id,
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarylyRemoved)
        {
            await SendResponseAsync(
                statusCode: GetMatchDetailResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }

        // Get role claim from access token.
        var roleClaim = context.HttpContext.User.FindFirstValue(claimType: "role");

        // Check if user is in role.
        var isUserRole = await userManager.IsInRoleAsync(user: founduser, role: roleClaim);

        // User is not in role.
        if (!isUserRole)
        {
            await SendResponseAsync(
                statusCode: GetMatchDetailResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );
        }
    }

    /// <summary>
    /// Sends a response with the specified status code.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="context"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private static Task SendResponseAsync(
        GetMatchDetailResponseStatusCode statusCode,
        IPreProcessorContext<GetMatchDetailRequest> context,
        CancellationToken ct
    )
    {
        var httpResponse = LazyGetMatchDetailHttpResponseMapper
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
