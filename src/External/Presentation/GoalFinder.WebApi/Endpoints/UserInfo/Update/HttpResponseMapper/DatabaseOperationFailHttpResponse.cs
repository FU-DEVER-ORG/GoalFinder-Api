using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper
{
    public class DatabaseOperationFailHttpResponse : UpdateUserInfoHttpResponse
    {
        internal DatabaseOperationFailHttpResponse(UpdateUserInfoResponse response) 
        {
            HttpCode = StatusCodes.Status500InternalServerError;
            AppCode = response.StatusCode.ToAppCode();
            ErrorMessages =
        [
            "Server error !!!"
        ];
        }

    }
}
