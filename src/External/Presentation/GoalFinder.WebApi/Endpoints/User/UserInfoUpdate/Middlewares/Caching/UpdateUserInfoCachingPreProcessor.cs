using FastEndpoints;
using GoalFinder.Application.Features.User.UpdateUserInfo;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Common;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Caching;

/// <summary>
///     Caching pre processor.
/// </summary>
internal sealed class UpdateUserInfoCachingPreProcessor : PreProcessor<
    UpdateUserInfoRequest,
    UpdateUserInfoStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdateUserInfoCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<UpdateUserInfoRequest> context,
        UpdateUserInfoStateBag state,
        CancellationToken ct)
    {
        if (context.HttpContext.ResponseStarted()) { return; }

        state.CacheKey = $"{nameof(UpdateUserInfoRequest)}_username_{context.Request.GetUserId()}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        var cacheModel = await cacheHandler.GetAsync<UpdateUserInfoHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct);

        if (!Equals(
                objA: cacheModel,
                objB: AppCacheModel<UpdateUserInfoHttpResponse>.NotFound))
        {
            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: cacheModel.Value.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }

    }
}
