using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Validation;

/// <summary>
///     Preprocessor for login validation.
/// </summary>
public sealed class LoginValidationPreProcessor : PreProcessor<LoginRequest, LoginResponse>
{
    public override Task PreProcessAsync(
        IPreProcessorContext<LoginRequest> context,
        LoginResponse state,
        CancellationToken ct)
    {
        if (context.HasValidationFailures)
        {
            var httpResponse = LazyLoginHttResponseMapper
                .Get()
                .Resolve(statusCode: LoginResponseStatusCode.INPUT_VALIDATION_FAIL)
                .Invoke(arg1: context.Request, arg2: state);

            context.HttpContext.Response.StatusCode = httpResponse.HttpCode;

            return context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                jsonSerializerContext: default,
                cancellation: ct);
        }

        return Task.CompletedTask;
    }
}
