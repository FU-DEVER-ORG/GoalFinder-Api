using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Match.GetAllMatches;

/// <summary>
///     Get All Football Matches Handler
/// </summary>
internal sealed class GetAllMatchesHandler
    : IFeatureHandler<GetAllMatchesRequest, GetAllMatchesResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMatchesHandler(IUnitOfWork unitOfWork)
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
        CancellationToken ct
    )
    {
        //Get all football matches
        var matches = await _unitOfWork.GetAllMatchesRepository.GetAllMatchesQueryAsync(
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetAllMatchesResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                FootballMatches = matches.Select(
                    matches => new GetAllMatchesResponse.Body.FootballMatch
                    {
                        Id = matches.Id,
                        PitchAddress = matches.PitchAddress,
                        MaxMatchPlayersNeed = matches.MaxMatchPlayersNeed,
                        PitchPrice = matches.PitchPrice,
                        Title = matches.Title,
                        Description = matches.Description,
                        MinPrestigeScore = matches.MinPrestigeScore,
                        StartTime = TimeZoneInfo.ConvertTimeFromUtc(
                            dateTime: matches.StartTime,
                            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                id: "SE Asia Standard Time"
                            )
                        ),
                        Address = matches.Address,
                        CompetitionLevel = matches.CompetitionLevel?.FullName,
                        TimeAgo = (int)
                            (
                                TimeZoneInfo.ConvertTimeFromUtc(
                                    dateTime: DateTime.UtcNow,
                                    destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                        id: "SE Asia Standard Time"
                                    )
                                )
                                - TimeZoneInfo.ConvertTimeFromUtc(
                                    dateTime: matches.CreatedAt,
                                    destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                        id: "SE Asia Standard Time"
                                    )
                                )
                            ).TotalMinutes,
                        HostId = matches.HostId,
                        HostName = ToHostName(matches),
                    }
                )
            }
        };
    }

    private string ToHostName(FootballMatch match)
    {
        if (
            !string.IsNullOrEmpty(match.UserDetail.FirstName)
            || !string.IsNullOrEmpty(match.UserDetail.LastName)
        )
        {
            return $"{match.UserDetail.FirstName} {match.UserDetail.LastName}";
        }
        else if (!string.IsNullOrEmpty(match.UserDetail.NickName))
        {
            return match.UserDetail.NickName;
        }

        return match.UserDetail.User.UserName;
    }
}
