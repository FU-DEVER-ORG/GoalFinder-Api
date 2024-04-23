using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.Middlewares.Validation
{
    internal class UpdateUserInfoValidationPreProcessor : PreProcessor<UpdateUserInfoRequest, UpdateUserInfoStateBag>
    {
        public override async Task PreProcessAsync(
            IPreProcessorContext<UpdateUserInfoRequest> context,
            UpdateUserInfoStateBag state,
            CancellationToken ct)
        {
            if (context.HasValidationFailures)
            {
                var httpResponse = LazyUpdateUserInfoHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL
                        }
                    );
                await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);

                context.HttpContext.MarkResponseStart();
            }
        }
    }
}
