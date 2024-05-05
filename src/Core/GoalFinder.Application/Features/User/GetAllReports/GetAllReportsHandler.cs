using System.Threading;
using GoalFinder.Application.Features.Match.GetAllMatches;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace GoalFinder.Application.Features.User.GetAllReports;

/// <summary>
///     Get All Reports Handler
/// </summary>
internal sealed class GetAllReportsHandler
    : IFeatureHandler<GetAllReportsRequest, GetAllReportsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllReportsHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllReportsResponse> ExecuteAsync(
        GetAllReportsRequest command,
        CancellationToken ct
    )
    {
        //Get all reports
        var reports = await _unitOfWork.GetAllReportsRepository.GetAllReportsQueryAsync(
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetAllReportsStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                MatchPlayers = reports.Select(
                    report => new GetAllReportsResponse.Body.MatchPlayer
                    {
                        MatchId = report.MatchId,
                        PlayerId = report.PlayerId,
                        PlayerName = $"{report.UserDetail?.FirstName} {report.UserDetail?.LastName}",
                        NumberOfReports = report.NumberOfReports,

                    }
                )
            }
        };

        
    }
}
