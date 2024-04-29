using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.CreateMatch;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.Common;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch.Middlewares.Caching;

/// <summary>
///     Caching pre processor.
/// </summary>
internal sealed class CreateMatchCachingPreProcessor
    : PreProcessor<CreateMatchRequest, CreateMatchStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CreateMatchCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<CreateMatchRequest> context,
        CreateMatchStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey = $"{nameof(CreateMatchRequest)}_username_{context.Request.GetHostId()}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        var cacheModel = await cacheHandler.GetAsync<CreateMatchHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (!Equals(objA: cacheModel, objB: AppCacheModel<CreateMatchHttpResponse>.NotFound))
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
