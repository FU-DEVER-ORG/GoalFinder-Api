using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Common;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Middleware.Caching;

/// <summary>
///     Caching pre processor
/// </summary>
internal sealed class ReportUserAfterMatchCachingPreProcessor
    : PreProcessor<EmptyRequest, ReportUserAfterMatchStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReportUserAfterMatchCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        ReportUserAfterMatchStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey =
            $"{nameof(ReportUserAfterMatchRequest)}_matchId_{state.AppRequest.GetFootballMatchId()}_playerId_{state.AppRequest.GetPlayerId()}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // get from cache
        var cacheModel = await cacheHandler.GetAsync<ReportUserAfterMatchHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (
            !Equals(
                objA: cacheModel,
                objB: AppCacheModel<ReportUserAfterMatchHttpResponse>.NotFound
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

            context.HttpContext.MarkResponseStart();
        }
    }
}
