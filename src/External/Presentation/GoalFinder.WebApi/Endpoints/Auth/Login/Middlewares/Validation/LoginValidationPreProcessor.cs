using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.Common;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Validation;

/// <summary>
///     Preprocessor for login validation.
/// </summary>
internal sealed class LoginValidationPreProcessor : PreProcessor<LoginRequest, LoginStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<LoginRequest> context,
        LoginStateBag state,
        CancellationToken ct
    )
    {
        if (context.HasValidationFailures)
        {
            LoginHttpResponse httpResponse;

            if (
                !Equals(
                    objA: context.ValidationFailures.Find(match: failure =>
                        failure.PropertyName.Equals(value: "SerializerErrors")
                    ),
                    objB: default
                )
            )
            {
                httpResponse = LazyLoginHttResponseMapper
                    .Get()
                    .Resolve(statusCode: LoginResponseStatusCode.INPUT_NOT_UNDERSTANDABLE)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = LoginResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        }
                    );
            }
            else
            {
                httpResponse = LazyLoginHttResponseMapper
                    .Get()
                    .Resolve(statusCode: LoginResponseStatusCode.INPUT_VALIDATION_FAIL)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new() { StatusCode = LoginResponseStatusCode.INPUT_VALIDATION_FAIL }
                    );
            }

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct
            );

            context.HttpContext.MarkResponseStart();
        }
    }
}
