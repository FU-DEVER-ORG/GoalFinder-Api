using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using System;
using System.Collections.Generic;

namespace GoalFinder.Application.Features.UserInfo.Update
{
    public sealed class UpdateUserInfoRequest : IFeatureRequest<UpdateUserInfoResponse>
    {
        public Guid UserId { get; init; }
        public string UserName { get; init; }

        public string LastName { get; init; }

        public string FirstName { get; init; }

        public string Description { get; init; } 

        public string Address { get; init; }

        public string AvatarUrl { get; init; }

        public string Experience { get; init; }
        public IEnumerable<UserPosition> Position { get; init; }
        public string CompetitionLevel { get; init; }


    }
}
