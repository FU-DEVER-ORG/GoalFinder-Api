using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Caching;

internal class ForgotPasswordCachingPreProcessor : PreProcessor<ForgotPasswordRequest, ForgotPasswordStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public ForgotPasswordCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<ForgotPasswordRequest> context, 
        ForgotPasswordStateBag state, 
        CancellationToken ct)
    {
        if (context.HttpContext.ResponseStarted()) { return; }
        state.CacheKey = $"{nameof(ForgotPasswordRequest)}_username_{context.Request.UserName}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        var cacheModel = await cacheHandler.GetAsync<ForgotPasswordHttpReponse>(
            key: state.CacheKey,
            cancellationToken: ct
            );
        if (!Equals(objA: cacheModel, 
            objB: AppCacheModel<ForgotPasswordHttpReponse>.NotFound))
        {
            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: cacheModel.Value.HttpCode,
                cancellation: ct
                );
            context.HttpContext.MarkResponseStart();
        }
    }
}
