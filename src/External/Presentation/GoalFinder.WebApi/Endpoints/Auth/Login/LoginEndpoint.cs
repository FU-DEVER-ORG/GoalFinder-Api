using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.Login;

public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    public IUnitOfWork UnitOfWork { get; set; }

    public override void Configure()
    {
        Get("test");
        AllowAnonymous();
    }

    public override async Task<LoginResponse> ExecuteAsync(
        LoginRequest req,
        CancellationToken ct)
    {
        //var response = await req.ExecuteAsync(ct: ct);

        HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        return new()
        {
            StatusCode = LoginResponseStatusCode.USER_IS_NOT_FOUND
        };
    }
}
