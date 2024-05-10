using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;

/// <summary>
/// Get User Profile By User Id
/// </summary>
public sealed class GetUserProfileByUserIdResponse : IFeatureResponse
{
    public GetUserProfileByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public User UserDetail { get; init; }

        public IEnumerable<FootballMatch> FootballMatches { get; init; }

        public sealed class User
        {
            public string NickName { get; init; }

            public string LastName { get; init; }

            public string FirstName { get; init; }

            public string Description { get; init; }

            public int PrestigeScore { get; init; }

            public string Address { get; init; }

            public string AvatarUrl { get; init; }

            public string Experience { get; init; }

            public string CompetitionLevel { get; init; }

            public IEnumerable<string> Positions { get; init; }
        }

        public sealed class FootballMatch
        {
            public Guid Id { get; init; }

            public string PitchAddress { get; init; }

            public int MaxMatchPlayersNeed { get; init; }

            public decimal PitchPrice { get; init; }

            public string Description { get; init; }

            public string StartTime { get; init; }

            public string Address { get; init; }

            public string CompetitionLevel { get; init; }
        }
    }
}
