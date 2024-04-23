using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper
{
    internal sealed class UserIsExstedHttpResponse : UpdateUserInfoHttpResponse
    {
        internal UserIsExstedHttpResponse(
            UpdateUserInfoRequest request, 
            UpdateUserInfoResponse response) 
        {
            HttpCode = StatusCodes.Status409Conflict;
            AppCode = response.StatusCode.ToAppCode();
            ErrorMessages =
        [
            $"User with username = {request.UserName} already exists"
        ];
        }
    }
}
