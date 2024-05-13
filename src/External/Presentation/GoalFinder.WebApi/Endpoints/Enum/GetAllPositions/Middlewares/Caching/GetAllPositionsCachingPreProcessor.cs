using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Middlewares.Caching;

/// <summary>
///     Caching pre processor.
/// </summary>
internal sealed class GetAllPositionsCachingPreProcessor
    : PreProcessor<EmptyRequest, GetAllPositionsStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllPositionsCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        GetAllPositionsStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        state.CacheKey = $"{nameof(GetAllPositions)}_dropdown";

        var cacheModel = await cacheHandler.GetAsync<GetAllPositionsHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (!Equals(objA: cacheModel, objB: AppCacheModel<GetAllPositionsHttpResponse>.NotFound))
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
