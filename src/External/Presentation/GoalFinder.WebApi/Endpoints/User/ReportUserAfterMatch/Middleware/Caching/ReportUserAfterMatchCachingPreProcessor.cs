using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Common;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Middleware.Caching;

/// <summary>
///     Caching pre processor
/// </summary>
internal sealed class ReportUserAfterMatchCachingPreProcessor
    : PreProcessor<ReportUserAfterMatchRequest, ReportUserAfterMatchStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReportUserAfterMatchCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<ReportUserAfterMatchRequest> context,
        ReportUserAfterMatchStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey =
            $"{nameof(ReportUserAfterMatchRequest)}_matchId_{context.Request.FootballMatchId}_userId_{context.Request.UserId}";

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
