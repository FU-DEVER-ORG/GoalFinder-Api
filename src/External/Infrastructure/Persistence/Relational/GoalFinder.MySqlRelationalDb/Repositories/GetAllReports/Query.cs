using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllReports;

internal sealed partial class GetAllReportsRepository
{
    public async Task<IEnumerable<MatchPlayer>> GetAllReportsQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _matchPlayers
            .AsNoTracking()
            .OrderBy(keySelector: report => report.FootballMatch.EndTime)
            .Select(report => new MatchPlayer()
            {
                MatchId = report.MatchId,
                PlayerId = report.PlayerId,
                NumberOfReports = report.NumberOfReports,
                UserDetail = new UserDetail()
                {
                    FirstName = report.UserDetail.FirstName,
                    LastName = report.UserDetail.LastName,
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<MatchPlayer>> GetMatchPlayerByMatchIdAsync(
        Guid matchId,
        CancellationToken cancellationToken
    )
    {
        return await _matchPlayers
            .AsNoTracking()
            .Where(predicate: matchPlayer => matchPlayer.MatchId == matchId)
            .Select(matchPlayer => new MatchPlayer
            {
                PlayerId = matchPlayer.PlayerId,
                NumberOfReports = matchPlayer.NumberOfReports
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<UserDetail> GetUserDetailByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _userDetails
            .AsNoTracking()
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(selector: userDetail => new UserDetail
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                PrestigeScore = userDetail.PrestigeScore,
                AvatarUrl = userDetail.AvatarUrl,
                CompetitionLevel = new() { FullName = userDetail.CompetitionLevel.FullName, },
                UserPositions = userDetail.UserPositions.Select(userPosition => new UserPosition
                {
                    Position = new() { FullName = userPosition.Position.FullName, }
                }),
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<FootballMatch> GetFootballMatchByIdQueryAsync(
        Guid matchId,
        CancellationToken cancellationToken
    )
    {
        return _footballMatch
            .AsNoTracking()
            .Where(predicate: footballMatch => footballMatch.Id == matchId)
            .Select(selector: footballMatch => new FootballMatch
            {
                PitchAddress = footballMatch.PitchAddress,
                MaxMatchPlayersNeed = footballMatch.MaxMatchPlayersNeed,
                PitchPrice = footballMatch.PitchPrice,
                Description = footballMatch.Description,
                StartTime = footballMatch.StartTime,
                EndTime = footballMatch.EndTime,
                Address = footballMatch.Address,
                CompetitionLevel = new() { FullName = footballMatch.CompetitionLevel.FullName, },
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
