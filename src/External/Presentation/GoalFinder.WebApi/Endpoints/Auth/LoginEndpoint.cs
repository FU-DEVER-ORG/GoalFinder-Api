using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth;

public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Get("auth/signin");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var response = await new LoginRequest().ExecuteAsync(ct: ct);

        await SendAsync(response, 200, ct);
    }
}
