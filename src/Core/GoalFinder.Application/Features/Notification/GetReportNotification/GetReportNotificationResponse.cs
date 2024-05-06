using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Notification.GetReportNotification;

/// <summary>
///     Get Notification Report Response.
/// </summary>
public sealed class GetReportNotificationResponse : IFeatureResponse
{
    public GetReportNotificationResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<ReportNotification> ReportNotifications { get; init; }

        public sealed class ReportNotification
        {
            public Guid MatchId { get; init; } 

            public DateTime EndTimeToReport { get; init; }

            public bool IsReported { get; init; }
        }
    }
}
