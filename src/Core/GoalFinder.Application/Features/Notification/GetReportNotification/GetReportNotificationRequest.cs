using System;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Notification.GetReportNotification;

/// <summary>
///     Get Report Notification Request
/// </summary>
public sealed class GetReportNotificationRequest : IFeatureRequest<GetReportNotificationResponse>
{
    private Guid _userId;

    public Guid GetUserId() => _userId;

    public void SetUserId(Guid userId) => _userId = userId;
}
