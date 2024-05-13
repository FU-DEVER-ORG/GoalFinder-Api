using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Match.GetMatchDetail;

/// <summary>
///     Get Match Detail Handler
/// </summary>

internal sealed class GetMatchDetailHandler
    : IFeatureHandler<GetMatchDetailRequest, GetMatchDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMatchDetailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetMatchDetailResponse> ExecuteAsync(
        GetMatchDetailRequest command,
        CancellationToken ct
    )
    {
        //  get football match detail
        var footballMatchInfo =
            await _unitOfWork.GetMatchDetailRepository.GetFootballMatchByIdQueryAsync(
                matchId: command.MatchId,
                cancellationToken: ct
            );

        //  if not found
        if (Equals(objA: footballMatchInfo, objB: default))
        {
            return new()
            {
                StatusCode = GetMatchDetailResponseStatusCode.FOOTBALL_MATCH_NOT_FOUND,
            };
        }

        return new()
        {
            StatusCode = GetMatchDetailResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                FootBallMatchDetail = new()
                {
                    HostOfMatch = new()
                    {
                        Id = footballMatchInfo.UserDetail.UserId,
                        HostAvatar = footballMatchInfo.UserDetail.AvatarUrl,
                        HostPrestigeScore = footballMatchInfo.UserDetail.PrestigeScore,
                        HostName = ToHostName(match: footballMatchInfo),
                    },
                    MatchInfor = new()
                    {
                        Id = footballMatchInfo.Id,
                        Address = footballMatchInfo.Address,
                        CompetitionLevel = footballMatchInfo.CompetitionLevel.FullName,
                        Description = footballMatchInfo.Description,
                        Title = footballMatchInfo.Title,
                        MaxMatchPlayersNeed = footballMatchInfo.MaxMatchPlayersNeed,
                        MinPrestigeScore = footballMatchInfo.MinPrestigeScore,
                        PitchAddress = footballMatchInfo.PitchAddress,
                        PitchPrice = footballMatchInfo.PitchPrice,
                        StartTime = footballMatchInfo.StartTime,
                        TimeAgo = (int)
                            (
                                TimeZoneInfo.ConvertTimeFromUtc(
                                    dateTime: DateTime.UtcNow,
                                    destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                        id: "SE Asia Standard Time"
                                    )
                                )
                                - TimeZoneInfo.ConvertTimeFromUtc(
                                    dateTime: footballMatchInfo.CreatedAt,
                                    destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                        id: "SE Asia Standard Time"
                                    )
                                )
                            ).TotalMinutes,
                    },
                },
                ParticipatedUser = footballMatchInfo
                    .MatchPlayers.Where(matchPlayer =>
                        matchPlayer.MatchPlayerJoiningStatus.FullName.Equals(value: "Joined")
                    )
                    .Select(matchPlayer => new GetMatchDetailResponse.Body.ParticipatingUser()
                    {
                        Id = matchPlayer.PlayerId,
                        PhoneNumber = matchPlayer.UserDetail.User.PhoneNumber,
                        PrestigeScore = matchPlayer.UserDetail.PrestigeScore,
                        UserAddress = matchPlayer.UserDetail.Address,
                        UserAvatar = matchPlayer.UserDetail.AvatarUrl,
                        UserCompetitionLevel = matchPlayer.UserDetail.CompetitionLevel.FullName,
                        UserName = ToUserName(matchPlayer),
                        UserPosition = matchPlayer.UserDetail.UserPositions.Select(
                            selector: userPosition => userPosition?.Position?.FullName
                        ),
                    }),
                CurrentPendingUser =
                    footballMatchInfo.HostId == command.GetUserId()
                        ? footballMatchInfo
                            .MatchPlayers.Where(matchPlayer =>
                                matchPlayer.MatchPlayerJoiningStatus.FullName.Equals(
                                    value: "Pending"
                                )
                            )
                            .Select(
                                matchPlayer => new GetMatchDetailResponse.Body.ParticipatingUser()
                                {
                                    Id = matchPlayer.PlayerId,
                                    PhoneNumber = matchPlayer.UserDetail.User.PhoneNumber,
                                    PrestigeScore = matchPlayer.UserDetail.PrestigeScore,
                                    UserAddress = matchPlayer.UserDetail.Address,
                                    UserAvatar = matchPlayer.UserDetail.AvatarUrl,
                                    UserCompetitionLevel = matchPlayer
                                        .UserDetail
                                        .CompetitionLevel
                                        .FullName,
                                    UserName = ToUserName(matchPlayer),
                                    UserPosition = matchPlayer.UserDetail.UserPositions.Select(
                                        selector: userPosition => userPosition?.Position?.FullName
                                    ),
                                }
                            )
                        : [],
                RejectedUsers =
                    footballMatchInfo.HostId == command.GetUserId()
                        ? footballMatchInfo
                            .MatchPlayers.Where(matchPlayer =>
                                matchPlayer.MatchPlayerJoiningStatus.FullName.Equals(
                                    value: "Rejected"
                                )
                            )
                            .Select(
                                matchPlayer => new GetMatchDetailResponse.Body.ParticipatingUser()
                                {
                                    Id = matchPlayer.PlayerId,
                                    PhoneNumber = matchPlayer.UserDetail.User.PhoneNumber,
                                    PrestigeScore = matchPlayer.UserDetail.PrestigeScore,
                                    UserAddress = matchPlayer.UserDetail.Address,
                                    UserAvatar = matchPlayer.UserDetail.AvatarUrl,
                                    UserCompetitionLevel = matchPlayer
                                        .UserDetail
                                        .CompetitionLevel
                                        .FullName,
                                    UserName = ToUserName(matchPlayer),
                                    UserPosition = matchPlayer.UserDetail.UserPositions.Select(
                                        selector: userPosition => userPosition?.Position?.FullName
                                    ),
                                }
                            )
                        : []
            }
        };
    }

    /// <summary>
    ///     To User Name
    /// </summary>
    /// <param name="match"></param>
    /// <returns></returns>

    private static string ToUserName(MatchPlayer match)
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

    private static string ToHostName(FootballMatch match)
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
