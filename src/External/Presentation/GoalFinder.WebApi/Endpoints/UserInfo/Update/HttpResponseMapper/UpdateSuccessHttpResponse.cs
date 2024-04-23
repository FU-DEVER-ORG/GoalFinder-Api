using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper
{
    internal sealed class UpdateSuccessHttpResponse : UpdateUserInfoHttpResponse
    {
        internal UpdateSuccessHttpResponse(UpdateUserInfoResponse response) 
        {
            HttpCode = StatusCodes.Status400BadRequest;
            AppCode = response.StatusCode.ToAppCode();
        }
    }
}
