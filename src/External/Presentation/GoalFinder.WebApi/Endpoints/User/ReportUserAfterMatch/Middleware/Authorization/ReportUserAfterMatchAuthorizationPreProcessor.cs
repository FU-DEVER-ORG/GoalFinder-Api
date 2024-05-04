using FastEndpoints;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.IdentityModel.JsonWebTokens;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Middleware.Authorization;


/// <summary>
///     Pre processor for report user after match authorization.
/// </summary>
internal sealed class ReportUserAfterMatchAuthorizationPreProcessor
    : PreProcessor<ReportUserAfterMatchRequest, ReportUserAfterMatchStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public ReportUserAfterMatchAuthorizationPreProcessor(
        IServiceScopeFactory serviceScopeFactory,
        TokenValidationParameters tokenValidationParameters
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<ReportUserAfterMatchRequest> context,
        ReportUserAfterMatchStateBag state,
        CancellationToken ct
    )
    {
        JsonWebTokenHandler jsonWebTokenHandler = new();

        // Validate access token.
        var validateTokenResult = await jsonWebTokenHandler.ValidateTokenAsync(
            token: context.HttpContext.Request.Headers.Authorization[0].Split(separator: " ")[1],
            validationParameters: _tokenValidationParameters
        );

        // Token is not valid.
        if (!validateTokenResult.IsValid)
        {
            await SendResponseAsync(
                statusCode: ReportUserAfterMatchResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );

            return;
        }

        // Get the jti claim from the access token.
        var jtiClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Jti
        );

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var unitOfWork = scope.Resolve<IUnitOfWork>();

        // Does refresh token exist by access token id.
        var isRefreshTokenFound =
            await unitOfWork.ReportUserAfterMatchRepository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(input: jtiClaim),
                cancellationToken: ct
            );

        // Refresh token is not found by access token id.
        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(
                statusCode: ReportUserAfterMatchResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );

            return;
        }

        var userManager = scope.Resolve<UserManager<Data.Entities.User>>();

        // Get the sub claim from the access token.
        var subClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Find user by user id.
        var foundUser = await userManager.FindByIdAsync(
            userId: Guid.Parse(input: subClaim).ToString()
        );

        // User is not found
        if (Equals(objA: foundUser, objB: default))
        {
            await SendResponseAsync(
                statusCode: ReportUserAfterMatchResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );

            return;
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await unitOfWork.ReportUserAfterMatchRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            await SendResponseAsync(
                statusCode: ReportUserAfterMatchResponseStatusCode.FORBIDDEN,
                context: context,
                ct: ct
            );

            return;
        }
    }

    private static Task SendResponseAsync(
        ReportUserAfterMatchResponseStatusCode statusCode,
        IPreProcessorContext<ReportUserAfterMatchRequest> context,
        CancellationToken ct
    )
    {
        var httpResponse = LazyReportUserAfterMatchHttpResponseMapper
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
