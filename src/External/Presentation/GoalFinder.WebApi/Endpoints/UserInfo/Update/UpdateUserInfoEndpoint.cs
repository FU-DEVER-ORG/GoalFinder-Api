using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.Middlewares.Validation;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update
{
    internal sealed class UpdateUserInfoEndpoint : Endpoint<UpdateUserInfoRequest, UpdateUserInfoHttpResponse, UpdateUserInfoMapper>
    {
        public override void Configure()
        {
            Put(routePatterns: "user/update");
            AllowAnonymous();
            DontThrowIfValidationFails();
            PreProcessor<UpdateUserInfoValidationPreProcessor>();
            PreProcessor<UpdateUserInfoCachingPreProcessor>();
            PostProcessor<UpdateUserInfoCachingPostProcessor>();
            DontThrowIfValidationFails();
            Description(builder: builder =>
            {
                builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
            });
            Summary(endpointSummary: summary =>
            {
                summary.Summary = "Endpoint for updating user information.";
                summary.Description = "This endpoint is used for updating user information purpose.";
                summary.ExampleRequest = new()
                {
                    UserName = "string",
                };
                summary.Response<UpdateUserInfoHttpResponse>(
                    description: "Represent successful operation response.",
                    example: new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = UpdateUserInfoResponseStatusCode.UPDATE_SUCCESS.ToAppCode(),
                        
                    });
            });
        }

        public override async Task<UpdateUserInfoHttpResponse> ExecuteAsync(
        UpdateUserInfoRequest req,
        CancellationToken ct)
        {
            var appResponse = await req.ExecuteAsync(ct: ct);

            var httpResponse = LazyUpdateUserInfoHttpResponseMapper
                .Get()
                .Resolve(statusCode: appResponse.StatusCode)
                .Invoke(arg1: req, arg2: appResponse);

            var httpResponseStatusCode = httpResponse.HttpCode;
            httpResponse.HttpCode = default;

            await SendAsync(
                response: httpResponse,
                statusCode: httpResponseStatusCode,
                cancellation: ct);

            httpResponse.HttpCode = httpResponseStatusCode;

            return httpResponse;
        }
    }
}
