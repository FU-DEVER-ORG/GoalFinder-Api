using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Match.GetMatchDetail;

/// <summary>
///    Get Match Detail Response
/// </summary>

public sealed class GetMatchDetailResponse : IFeatureResponse
{
    public GetMatchDetailResponseStatusCode StatusCode { get; set; }

    public Body ResponseBody { get; set; }

    public sealed class Body
    {
        public FootballMatchDetail FootBallMatchDetail { get; set; }

        public IEnumerable<ParticipatingUser> ParticipatedUser { get; set; }

        public IEnumerable<ParticipatingUser> CurrentPendingUser { get; set; }

        public IEnumerable<ParticipatingUser> RejectedUsers { get; set; }

        public sealed class FootballMatchDetail
        {
            public HostUser HostOfMatch { get; set; }

            public ThisFootballMatchInfor MatchInfor { get; set; }

            public sealed class HostUser
            {
                public Guid Id { get; set; }
                public string HostName { get; set; }
                public string HostAvatar { get; set; }
                public int HostPrestigeScore { get; set; }
            };

            public sealed class ThisFootballMatchInfor
            {
                public Guid Id { get; init; }

                public string PitchAddress { get; init; }

                public int MaxMatchPlayersNeed { get; init; }

                public decimal PitchPrice { get; init; }

                public string Title { get; init; }

                public string Description { get; init; }

                public int MinPrestigeScore { get; init; }

                public DateTime StartTime { get; init; }

                public string Address { get; init; }

                public string CompetitionLevel { get; init; }

                public int TimeAgo { get; init; }
            }
        };

        public sealed class ParticipatingUser
        {
            public Guid Id { get; set; }

            public string UserName { get; set; }

            public string UserAvatar { get; set; }

            public int PrestigeScore { get; set; }

            public IEnumerable<string> UserPosition { get; set; }

            public string UserCompetitionLevel { get; set; }

            public string PhoneNumber { get; set; }

            public string UserAddress { get; set; }
        }
    }
}
