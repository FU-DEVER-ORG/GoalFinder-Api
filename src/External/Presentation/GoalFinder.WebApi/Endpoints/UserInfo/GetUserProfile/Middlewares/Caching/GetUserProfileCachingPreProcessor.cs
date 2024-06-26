﻿using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Middleware.Caching;

/// <summary>
///     Caching pre processor for get user profile feature.
/// </summary>
internal sealed class GetUserProfileCachingPreProcessor
    : PreProcessor<GetUserProfileRequest, GetUserProfileStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetUserProfileCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<GetUserProfileRequest> context,
        GetUserProfileStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey = $"{nameof(GetUserProfileRequest)}_username_{context.Request.NickName}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // get from cache
        var cacheModel = await cacheHandler.GetAsync<GetUserProfileHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        // send response
        if (!Equals(objA: cacheModel, objB: AppCacheModel<GetUserProfileHttpResponse>.NotFound))
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
