using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Notification.GetReportNotification;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Common;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Middlewares.Caching;

/// <summary>
///     Post-processor for get report notification caching.
/// </summary>
internal sealed class GetReportNotificationCachingPostProcessor
    : PostProcessor<EmptyRequest, GetReportNotificationStateBag, GetReportNotificationHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetReportNotificationCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetReportNotificationHttpResponse> context,
        GetReportNotificationStateBag state,
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
                value: GetReportNotificationResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
            )
        )
        {
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
