using FastEndpoints;
using GoalFinder.Application.Features.User.GetAllReports;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.GetAllReports.Common;
using GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.Middleware.Caching;

/// <summary>
///     Post-processor for get all reports caching.
/// </summary>
internal sealed class GetAllReportsCachingPostProcessor
    : PostProcessor<EmptyRequest, GetAllReportsStateBag, GetAllReportsHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllReportsCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetAllReportsHttpResponse> context,
        GetAllReportsStateBag state,
        CancellationToken ct
    )
    {
        if (Equals(objA: context.Response, objB: default))
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Set new cache if current app code is suitable.
        if (
            context.Response.AppCode.Equals(
            value: GetAllReportsStatusCode.OPERATION_SUCCESS.ToAppCode())
        ){
            // Caching the return value.
            await cacheHandler.SetAsync(
                key: state.CacheKey,
                value: context.Response,
                new()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                        seconds: state.CacheDurationInSeconds
                    )
                },
                cancellationToken: ct
            );
        }
    }
}
