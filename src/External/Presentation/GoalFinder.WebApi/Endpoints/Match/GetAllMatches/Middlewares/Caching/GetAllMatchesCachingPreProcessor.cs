using FastEndpoints;
using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Common;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Middleware.Caching;

/// <summary>
///     Caching pre processor for get all football matches feature.
/// </summary>
internal sealed class GetAllMatchesCachingPreProcessor : PreProcessor<
    EmptyRequest,
    GetAllMatchesStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllMatchesCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        GetAllMatchesStateBag state,
        CancellationToken ct)
    {
        state.CacheKey = $"{nameof(GetAllMatchesRequest)}_matches";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // get from cache
        var cacheModel = await cacheHandler.GetAsync<GetAllMatchesHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct);

        // send response
        if (!Equals(
            objA: cacheModel,
            objB: AppCacheModel<GetAllMatchesHttpResponse>.NotFound))
        {
            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: cacheModel.Value.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
