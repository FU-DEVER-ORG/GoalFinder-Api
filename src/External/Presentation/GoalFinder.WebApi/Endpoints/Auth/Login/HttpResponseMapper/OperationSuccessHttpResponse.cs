using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse : LoginHttpResponse
{
    internal OperationSuccessHttpResponse(LoginResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = response.StatusCode.ToAppCode();
        Body = response.ResponseBody;
    }
}
