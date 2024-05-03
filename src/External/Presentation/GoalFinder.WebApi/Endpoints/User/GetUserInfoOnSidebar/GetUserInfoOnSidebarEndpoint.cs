﻿using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.Common;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.Middlewares.Authorization;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.Middlewares.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar;

/// <summary>
///     Endpoint for updating user information.
/// </summary>
internal sealed class GetUserInfoOnSidebarEndpoint
    : Endpoint<EmptyRequest, GetUserInfoOnSidebarHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "user/sidebar");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<GetUserInfoOnSidebarAuthorizationPreProcessor>();
        PreProcessor<GetUserInfoOnSidebarCachingPreProcessor>();
        PostProcessor<GetUserInfoOnSidebarCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(
                StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status403Forbidden
            );
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for updating user information.";
            summary.Description = "This endpoint is used for updating user information purpose.";
            summary.ExampleRequest = new() { };
            summary.Response<GetUserInfoOnSidebarHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetUserInfoOnSidebarResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetUserInfoOnSidebarResponse.ResponseBody
                    {
                        Area = "string",
                        UserName = "string",
                        PrestigeScore = default
                    }
                }
            );
        });
    }

    public override async Task<GetUserInfoOnSidebarHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<GetUserInfoOnSidebarStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetUserInfoOnSidebarHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: stateBag.AppRequest, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct
        );

        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}