using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper
{
    internal sealed class UserNotFoundHttpResponse : UpdateUserInfoHttpResponse
    {
        internal UserNotFoundHttpResponse(UpdateUserInfoResponse response)
        {
            HttpCode = StatusCodes.Status404NotFound;
            AppCode = response.StatusCode.ToAppCode();
        }
    }
}
