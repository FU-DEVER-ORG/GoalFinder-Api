using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using GoalFinder.WebApi.Shared.Response;
using GoalFinder.WebApi.Shared.AppCodes;

namespace GoalFinder.WebApi.Shared.Middlewares;

/// <summary>
///     Global Exception Handler
/// </summary>
internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            value: new OtherPurposeResponse
            {
                AppCode = OtherPurposeAppCode.SERVER_ERROR.ToString(),
                ErrorMessages =
                [
                    "Server has encountered an error !!",
                    "Please contact admin for support.",
                ]
            },
            cancellationToken: cancellationToken);

        await httpContext.Response.CompleteAsync();

        return true;
    }
}