using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.UpdateUserInfo;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Authorization;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate;

/// <summary>
///     Endpoint for updating user information.
/// </summary>
internal sealed class UpdateUserInfoEndpoint
    : Endpoint<UpdateUserInfoRequest, UpdateUserInfoHttpResponse>
{
    public override void Configure()
    {
        Patch(routePatterns: "user/update");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<UpdateUserInfoAuthorizationPreProcessor>();
        PreProcessor<UpdateUserInfoValidationPreProcessor>();
        PreProcessor<UpdateUserInfoCachingPreProcessor>();
        PostProcessor<UpdateUserInfoCachingPostProcessor>();
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
            summary.ExampleRequest = new()
            {
                NickName = "string",
                LastName = "string",
                FirstName = "string",
                Description = "string",
                Address = "string",
                AvatarUrl = "string",
                BackgroundUrl = "string",
                ExperienceId = Guid.Empty,
                PositionIds = [],
                CompetitionLevelId = Guid.Empty
            };
            summary.Response<UpdateUserInfoHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = UpdateUserInfoResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<UpdateUserInfoHttpResponse> ExecuteAsync(
        UpdateUserInfoRequest req,
        CancellationToken ct
    )
    {
        req.SetUserId(
            userId: Guid.Parse(
                input: HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
            )
        );

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
            cancellation: ct
        );

        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
