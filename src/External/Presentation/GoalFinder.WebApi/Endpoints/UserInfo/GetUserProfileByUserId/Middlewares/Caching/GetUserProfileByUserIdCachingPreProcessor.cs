using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Middlewares.Caching;

/// <summary>
///     Pre Processor for Caching
/// </summary>

internal sealed class GetUserProfileByUserIdCachingPreProcessor
    : PreProcessor<GetUserProfileByUserIdRequest, GetUserProfileByUserIdStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetUserProfileByUserIdCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<GetUserProfileByUserIdRequest> context,
        GetUserProfileByUserIdStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey = $"{nameof(GetUserProfileByUserId)}_userId_{context.Request.Id}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // get from cache
        var cacheModel = await cacheHandler.GetAsync<GetUserProfileByUserIdHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        // send response
        if (
            !Equals(
                objA: cacheModel,
                objB: AppCacheModel<GetUserProfileByUserIdHttpResponse>.NotFound
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
