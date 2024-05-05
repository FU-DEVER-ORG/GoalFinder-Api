using FastEndpoints;
using GoalFinder.Application.Features.User.GetAllReports;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.GetAllReports.Common;
using GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.Middleware.Caching;

/// <summary>
///     Pre-processor for get all reports caching.
/// </summary>
internal sealed class GetAllReportsCachingPreProcessor
    : PreProcessor<EmptyRequest, GetAllReportsStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllReportsCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        GetAllReportsStateBag state,
        CancellationToken ct)
    {
        state.CacheKey = $"{nameof(GetAllReportsRequest)}_reports";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        //get from cache
        var cacheModel = await cacheHandler.GetAsync<GetAllReportsHttpResponse>(
                key: state.CacheKey,
                cancellationToken: ct
        );

        //send response
        if (!Equals(objA: cacheModel, objB: AppCacheModel<GetAllReportsHttpResponse>.NotFound))
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
