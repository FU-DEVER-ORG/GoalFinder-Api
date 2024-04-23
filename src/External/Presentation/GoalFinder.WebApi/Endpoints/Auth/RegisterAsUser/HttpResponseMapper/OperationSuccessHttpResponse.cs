using Microsoft.AspNetCore.Http;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;
using GoalFinder.Application.Features.Auth.Register;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Register as user response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse : RegisterAsUserHttpResponse
{
    internal OperationSuccessHttpResponse(RegisterAsUserResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = response.StatusCode.ToAppCode();
    }
}
