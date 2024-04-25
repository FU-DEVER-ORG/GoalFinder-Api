using FastEndpoints;
using GoalFinder.Application.Features.User.UpdateUserInfo;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Common;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Authorization;

/// <summary>
///     Pre-processor for <see cref="UpdateUserInfoRequest"/>
/// </summary>
internal sealed class UpdateUserInfoAuthorizationPreProcessor : PreProcessor<
    UpdateUserInfoRequest,
    UpdateUserInfoStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public UpdateUserInfoAuthorizationPreProcessor(
        IServiceScopeFactory serviceScopeFactory,
        TokenValidationParameters tokenValidationParameters)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<UpdateUserInfoRequest> context,
        UpdateUserInfoStateBag state,
        CancellationToken ct)
    {
        JsonWebTokenHandler jsonWebTokenHandler = new();

        // Validate access token.
        var validateTokenResult = await jsonWebTokenHandler.ValidateTokenAsync(
            token: context.HttpContext.Request.Headers.Authorization[0].Split(separator: " ")[1],
            validationParameters: _tokenValidationParameters);

        // Token is not valid.
        if (!validateTokenResult.IsValid)
        {
            await SendResponseAsync(
                statusCode: UpdateUserInfoResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct);
        }

        // Get the jti claim from the access token.
        var jtiClaim = context
            .HttpContext
            .User
            .FindFirstValue(claimType: JwtRegisteredClaimNames.Jti);

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var unitOfWork = scope.Resolve<IUnitOfWork>();

        // Does refresh token exist by access token id.
        var isRefreshTokenFound = await unitOfWork.UpdateUserInfoRepository
            .IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(input: jtiClaim),
                cancellationToken: ct);

        // Refresh token is not found by access token id.
        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(
                statusCode: UpdateUserInfoResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct);
        }

        var userManager = scope.Resolve<UserManager<Data.Entities.User>>();

        // Get the sub claim from the access token.
        var subClaim = context
            .HttpContext
            .User
            .FindFirstValue(claimType: JwtRegisteredClaimNames.Sub);

        // Find user by user id.
        var foundUser = await userManager.FindByIdAsync(userId: Guid
            .Parse(input: subClaim)
            .ToString());

        // User is not found
        if (Equals(objA: foundUser, objB: default))
        {
            await SendResponseAsync(
                statusCode: UpdateUserInfoResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct);
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved = await unitOfWork.UpdateUserInfoRepository
            .IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct);

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            await SendResponseAsync(
                statusCode: UpdateUserInfoResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct);
        }

        // Get the role claim from the access token.
        var roleClaim = context
            .HttpContext
            .User
            .FindFirstValue(claimType: "role");

        // Is user in role.
        var isUserInRole = await userManager.IsInRoleAsync(
            user: foundUser,
            role: roleClaim);

        // User is not in role.
        if (!isUserInRole)
        {
            await SendResponseAsync(
                statusCode: UpdateUserInfoResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct);
        }
    }

    private static Task SendResponseAsync(
        UpdateUserInfoResponseStatusCode statusCode,
        IPreProcessorContext<UpdateUserInfoRequest> context,
        CancellationToken ct)
    {
        var httpResponse = LazyUpdateUserInfoHttpResponseMapper
            .Get()
            .Resolve(statusCode: statusCode)
            .Invoke(
                arg1: context.Request,
                arg2: new()
                {
                    StatusCode = statusCode
                });

        context.HttpContext.MarkResponseStart();

        return context.HttpContext.Response.SendAsync(
            response: httpResponse,
            statusCode: httpResponse.HttpCode,
            cancellation: ct);
    }
}
