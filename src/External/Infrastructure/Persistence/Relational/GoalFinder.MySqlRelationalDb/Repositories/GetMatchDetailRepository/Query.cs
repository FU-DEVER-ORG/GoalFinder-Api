using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetMatchDetailRepository;

/// <summary>
///     Implementation of IGetMatchDetailRepository
/// </summary>
internal sealed partial class GetMatchDetailRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens.AnyAsync(
            predicate: refreshToken => refreshToken.AccessTokenId == accessTokenId,
            cancellationToken: cancellationToken
        );
    }

    public async Task<FootballMatch> GetFootballMatchByIdQueryAsync(
        Guid matchId,
        CancellationToken cancellationToken
    )
    {
        return await _footballMatches
            .AsNoTracking()
            .Where(predicate: footballMatch => footballMatch.Id == matchId)
            .Select(selector: footballMatch => new FootballMatch()
            {
                Id = footballMatch.Id,
                Address = footballMatch.Address,
                CompetitionLevel = new CompetitionLevel()
                {
                    FullName = footballMatch.CompetitionLevel.FullName,
                },
                Description = footballMatch.Description,
                Title = footballMatch.Title,
                HostId = footballMatch.HostId,
                UserDetail = new UserDetail()
                {
                    UserId = footballMatch.UserDetail.UserId,
                    FirstName = footballMatch.UserDetail.FirstName,
                    LastName = footballMatch.UserDetail.LastName,
                    AvatarUrl = footballMatch.UserDetail.AvatarUrl,
                    NickName = footballMatch.UserDetail.NickName,
                    PrestigeScore = footballMatch.UserDetail.PrestigeScore,
                },
                PitchAddress = footballMatch.PitchAddress,
                MinPrestigeScore = footballMatch.MinPrestigeScore,
                CreatedAt = footballMatch.CreatedAt,
                PitchPrice = footballMatch.PitchPrice,
                StartTime = footballMatch.StartTime,
                EndTime = footballMatch.EndTime,
                MaxMatchPlayersNeed = footballMatch.MaxMatchPlayersNeed,
                MatchPlayers = footballMatch.MatchPlayers.Select(matchPlayer => new MatchPlayer
                {
                    UserDetail = new UserDetail()
                    {
                        NickName = matchPlayer.UserDetail.NickName,
                        FirstName = matchPlayer.UserDetail.FirstName,
                        LastName = matchPlayer.UserDetail.LastName,
                        Address = matchPlayer.UserDetail.Address,
                        BackgroundUrl = matchPlayer.UserDetail.BackgroundUrl,
                        AvatarUrl = matchPlayer.UserDetail.AvatarUrl,
                        PrestigeScore = matchPlayer.UserDetail.PrestigeScore,
                        CompetitionLevel = new CompetitionLevel()
                        {
                            FullName = matchPlayer.UserDetail.CompetitionLevel.FullName
                        },
                        UserPositions = matchPlayer.UserDetail.UserPositions.Select(
                            userPosition => new UserPosition
                            {
                                Position = new() { FullName = userPosition.Position.FullName }
                            }
                        ),
                        Experience = new Experience()
                        {
                            FullName = matchPlayer.UserDetail.Experience.FullName,
                        },
                        User = new User()
                        {
                            PhoneNumber = matchPlayer.UserDetail.User.PhoneNumber,
                            UserName = matchPlayer.UserDetail.User.UserName
                        }
                    },
                    MatchPlayerJoiningStatus = new MatchPlayerJoiningStatus()
                    {
                        FullName = matchPlayer.MatchPlayerJoiningStatus.FullName
                    },
                    PlayerId = matchPlayer.PlayerId,
                })
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
