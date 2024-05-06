using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Middlewares.Caching;

/// <summary>
///     Caching pre processor.
/// </summary>
internal sealed class GetAllCompetitionLevelsCachingPreProcessor
    : PreProcessor<EmptyRequest, GetAllCompetitionLevelsStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllCompetitionLevelsCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        GetAllCompetitionLevelsStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        state.CacheKey = $"{nameof(GetAllCompetitionLevels)}_dropdown";

        var cacheModel = await cacheHandler.GetAsync<GetAllCompetitionLevelsHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (!Equals(objA: cacheModel, objB: AppCacheModel<GetAllCompetitionLevelsHttpResponse>.NotFound))
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
