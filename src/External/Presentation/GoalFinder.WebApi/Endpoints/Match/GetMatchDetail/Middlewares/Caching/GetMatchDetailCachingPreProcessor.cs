using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetMatchDetail;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Caching;

/// <summary>
/// Pre-processor for caching
/// </summary>
///
internal sealed class GetMatchDetailCachingPreProcessor
    : PreProcessor<GetMatchDetailRequest, GetMatchDetailStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetMatchDetailCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<GetMatchDetailRequest> context,
        GetMatchDetailStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey = $"{nameof(GetMatchDetail)}._matchId_{context.Request.MatchId}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        var cacheModel = await cacheHandler.GetAsync<GetMatchDetailHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (!Equals(objA: cacheModel, objB: AppCacheModel<GetMatchDetailHttpResponse>.NotFound))
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
