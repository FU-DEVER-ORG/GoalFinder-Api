using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword
{
    public class ForgotPasswordEndpoint : Endpoint<ForgotPasswordRequest, ForgotPasswordHttpReponse>
    {
        public override void Configure()
        {
            Post(routePatterns: "auth/forgot-password"); 
            PreProcessor<ForgotPasswordValidationPreProcessor>();
        }
        public override async Task<ForgotPasswordHttpReponse> ExecuteAsync(
            ForgotPasswordRequest req, CancellationToken ct)
        {
            var appResponse = await req.ExecuteAsync(ct: ct);

            var httpResponse = LazyForgotPasswordHttpResponseMapper
                .Get()
                .Resolve(statusCode: appResponse.StatusCode)
                .Invoke(arg1: req, arg2: appResponse);

            await SendAsync(
            response: httpResponse,
            statusCode: httpResponse.HttpCode,
            cancellation: ct);
            return default;
        }
    }
}
