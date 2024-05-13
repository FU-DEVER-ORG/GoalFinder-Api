using GoalFinder.Application.Features.Notification.GetReportNotification;

namespace GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Common;

/// <summary>
///     Represents the report notification state bag.
/// </summary>
internal sealed class GetReportNotificationStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;

    internal GetReportNotificationRequest AppRequest { get; } = new();
}
