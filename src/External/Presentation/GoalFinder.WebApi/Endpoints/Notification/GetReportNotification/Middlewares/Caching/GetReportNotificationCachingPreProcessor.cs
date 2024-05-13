using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Notification.GetReportNotification;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Common;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Middlewares.Caching;

/// <summary>
///     Caching pre get report notification matches feature.
/// </summary>
internal sealed class GetReportNotificationCachingPreProcessor
    : PreProcessor<EmptyRequest, GetReportNotificationStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetReportNotificationCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<EmptyRequest> context,
        GetReportNotificationStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        state.AppRequest.SetUserId(
            userId: Guid.Parse(
                input: context.HttpContext.User.FindFirstValue(
                    claimType: JwtRegisteredClaimNames.Sub
                )
            )
        );

        state.CacheKey =
            $"{nameof(GetReportNotificationRequest)}_userId_{state.AppRequest.GetUserId()}";

        var cacheModel = await cacheHandler.GetAsync<GetReportNotificationHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (
            !Equals(
                objA: cacheModel,
                objB: AppCacheModel<GetReportNotificationHttpResponse>.NotFound
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
        }
    }
}
