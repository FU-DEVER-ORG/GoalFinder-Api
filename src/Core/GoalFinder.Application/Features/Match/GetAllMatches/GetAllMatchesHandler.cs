using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Match.GetAllMatches;

/// <summary>
///     Get All Football Matches Handler
/// </summary>
internal sealed class GetAllMatchesHandler : IFeatureHandler<
    GetAllMatchesRequest,
    GetAllMatchesResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMatchesHandler(
        IUnitOfWork unitOfWork)
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
    public async Task<GetAllMatchesResponse> ExecuteAsync(
        GetAllMatchesRequest command,
        CancellationToken ct)
    {
        //Get all football matches
        var matches = await _unitOfWork.GetAllMatchesRepository.GetAllMatchesQueryAsync(cancellationToken: ct);

        return new()
        {
            StatusCode = GetAllMatchesResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                FootballMatches = matches.Select(m => new GetAllMatchesResponse.Body.FootballMatch()
                {
                    Id = m.Id,
                    PitchAddress = m.PitchAddress,
                    MaxMatchPlayersNeed = m.MaxMatchPlayersNeed,
                    PitchPrice = m.PitchPrice,
                    Description = m.Description,
                    MinPrestigeScore = m.MinPrestigeScore,
                    StartTime = m.StartTime.ToLocalTime().ToString(),
                    Address = m.Address,
                    CompetitionLevel = m.CompetitionLevel?.FullName,
                    TimeAgo = (DateTime.UtcNow.ToLocalTime() - m.CreatedAt.ToLocalTime()).ToString(),
                    HostId = m.HostId,
                    HostName = $"{m.UserDetail?.FirstName} {m.UserDetail?.LastName}",
                })
            }
        };
    }
}



