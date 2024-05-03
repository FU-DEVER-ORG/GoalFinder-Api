using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.GetDropdownAvatar;
using GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.Common;
using GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.Middlewares.Authorization;
using GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.Middlewares.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar;

/// <summary>
///     Endpoint for get dropdown avatar .
/// </summary>
internal sealed class GetDropdownAvatarEndpoint
    : Endpoint<EmptyRequest, GetDropdownAvatarHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "user/dropdown-avatar");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<GetDropdownAvatarAuthorizationPreProcessor>();
        PreProcessor<GetDropdownAvatarCachingPreProcessor>();
        PostProcessor<GetDropdownAvatarCachingPostProcessor>();
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
            summary.Response<GetDropdownAvatarHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetDropdownAvatarResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetDropdownAvatarResponse.ResponseBody
                    {
                        UserName = "string",
                        FirstName = "string",
                        LastName = "string"
                    }
                }
            );
        });
    }

    public override async Task<GetDropdownAvatarHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<GetDropdownAvatarStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetDropdownAvatarHttpResponseMapper
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
