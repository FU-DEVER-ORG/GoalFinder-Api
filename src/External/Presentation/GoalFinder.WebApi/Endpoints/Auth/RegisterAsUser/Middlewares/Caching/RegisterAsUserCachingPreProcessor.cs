﻿using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Register;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Common;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Middlewares.Caching;

/// <summary>
///     Pre-processor for register as user caching.
/// </summary>

internal sealed class RegisterAsUserCachingPreProcessor
    : PreProcessor<RegisterAsUserRequest, RegisterAsUserStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RegisterAsUserCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<RegisterAsUserRequest> context,
        RegisterAsUserStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey = $"{nameof(RegisterAsUserHttpResponse)}_username_{context.Request.Email}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Retrieve from cache.
        var cacheModel = await cacheHandler.GetAsync<RegisterAsUserHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        // Cache value does not exist.
        if (!Equals(objA: cacheModel, objB: AppCacheModel<RegisterAsUserHttpResponse>.NotFound))
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
