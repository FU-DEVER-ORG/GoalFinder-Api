using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetMatchesUpcoming;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Middlewares.Caching;

/// <summary>
///     Caching pre processor.
/// </summary>
internal sealed class GetMatchesUpcomingCachingPreProcessor
    : PreProcessor<EmptyRequest, GetMatchesUpcomingStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetMatchesUpcomingCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        GetMatchesUpcomingStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        state.AppRequest.SetUserId(
            userId: Guid.Parse(
                input: context.HttpContext.User.FindFirstValue(
                    claimType: JwtRegisteredClaimNames.Sub
                )
            )
        );

        state.CacheKey =
            $"{nameof(GetMatchesUpcomingRequest)}_userId_{state.AppRequest.GetUserId()}";

        var cacheModel = await cacheHandler.GetAsync<GetMatchesUpcomingHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (
            !Equals(
                objA: cacheModel,
                objB: AppCacheModel<GetMatchesUpcomingHttpResponse>.NotFound
            )
        )
        {
            var httpCode = cacheModel.Value.HttpCode;
            cacheModel.Value.HttpCode = default;

            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: httpCode,
                cancellation: ct
            );
        }
    }
}
