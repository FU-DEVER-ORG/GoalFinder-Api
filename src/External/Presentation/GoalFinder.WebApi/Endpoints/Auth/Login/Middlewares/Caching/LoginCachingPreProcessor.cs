﻿using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.Login.Common;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Caching;

/// <summary>
///     Pre-processor for login endpoint.
/// </summary>
internal sealed class LoginCachingPreProcessor : PreProcessor<LoginRequest, LoginStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LoginCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<LoginRequest> context,
        LoginStateBag state,
        CancellationToken ct)
    {
        if (context.HttpContext.ResponseStarted()) { return; }

        state.CacheKey = $"{nameof(LoginHttpResponse)}_username_{context.Request.Username}_password_{context.Request.Password}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Retrieve from cache.
        var cacheModel = await cacheHandler.GetAsync<LoginHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: AppCacheModel<LoginHttpResponse>.NotFound))
        {
            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: cacheModel.Value.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
