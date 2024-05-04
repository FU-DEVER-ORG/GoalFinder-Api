using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using System;
using System.Collections.Generic;
using static GoalFinder.Application.Features.User.GetUserInfoOnSidebar.GetUserInfoOnSidebarResponse;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
///      Report user after match response
/// </summary>
public sealed class ReportUserAfterMatchResponse : IFeatureResponse
{
    public ReportUserAfterMatchResponseStatusCode StatusCode { get; set; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {
        public List<UserDetail> UsersUpdatedPrestigeScore { get; init; }
    }
}
