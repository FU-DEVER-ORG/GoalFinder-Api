using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.Update;
using GoalFinder.Data.Entities;
using GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others;
using GoalFinder.WebApi.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update
{
    public class UpdateUserInfoMapper : Mapper<UpdateUserInfoRequest, UpdateUserInfoHttpResponse, UpdateUserInfoDTO>
    {
        public override UpdateUserInfoDTO ToEntity(UpdateUserInfoRequest r)
        {
            return new()
            {
                UserId = r.UserId,
                UserName = r.UserName,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Description = r.Description,
                Address = r.Address,
                AvatarUrl = r.AvatarUrl,
                Experience = r.Experience,
                //Position = GetPositionNames(r.Position.ToList()),
                CompetitionLevel = r.CompetitionLevel
            };
        }



        public List<string> GetPositionNames(List<UserPosition> userPositions)
        {
            var positionNames = userPositions
                .Select(up => up.Position.FullName) 
                .ToList();

            return positionNames;
        }

    }
}
