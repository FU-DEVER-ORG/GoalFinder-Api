using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Notification.GetReportNotification;

/// <summary>
///     Get Report Notification Handler
/// </summary>
internal sealed class GetReportNotificationHandler
    : IFeatureHandler<GetReportNotificationRequest, GetReportNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetReportNotificationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="command">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<GetReportNotificationResponse> ExecuteAsync(
        GetReportNotificationRequest command,
        CancellationToken ct
    )
    {
        //Get all football matches with upper block time 
        var matches = await _unitOfWork.GetReportNotificationRepository.GetMatchesWitUpperBlockTimeByUserId(
            userID : command.GetUserId(),
            currenTime: DateTime.UtcNow,
            cancellationToken: ct
        );

        if (!matches.Any())
        {
            return new() { StatusCode = GetReportNotificationResponseStatusCode.NO_NOTIFICATION_EXITS};
        }

        return new()
        {
            StatusCode = GetReportNotificationResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                ReportNotifications = matches.Select(
                    matches => new GetReportNotificationResponse.Body.ReportNotification
                    {
                        EndTimeToReport = matches.EndTime.AddDays(1),
                        MatchId = matches.Id,
                        IsReported = false,
                    }
                )
            }
        };
    }
}
