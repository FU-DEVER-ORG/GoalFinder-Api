using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.Login.Common;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.Middlewares.Caching
{
    internal sealed class UpdateUserInfoCachingPreProcessor : PreProcessor<UpdateUserInfoRequest, UpdateUserInfoStateBag>
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
            state.CacheKey = $"{nameof(UpdateUserInfoRequest)}_username_{context.Request.UserName}";

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
}
