using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile;

internal sealed partial class GetUserProfileRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _userDetails
            .AnyAsync(
                predicate:
                    userDetail => userDetail.UserId == userId &&
                    userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
                    userDetail.RemovedAt != DateTime.MinValue,
                cancellationToken: cancellationToken);
    }

    public Task<UserDetail> GetUserDetailAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _userDetails
            .AsNoTracking()
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(selector: userDetail => new UserDetail
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                Description = userDetail.Description,
                PrestigeScore = userDetail.PrestigeScore,
                Address = userDetail.Address,
                AvatarUrl = userDetail.AvatarUrl,
                Experience = new()
                {
                    FullName = userDetail.Experience.FullName,
                },
                CompetitionLevel = new()
                {
                    FullName = userDetail.CompetitionLevel.FullName,
                },
                UserPositions = userDetail.UserPositions.Select(
                    userPosition => new UserPosition
                    {
                        Position = new()
                        {
                            FullName = userPosition.Position.FullName,
                        }
                    }),
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<FootballMatch>> GetFootballMatchByIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _matchPlayer
            .AsNoTracking()
            .Where(predicate: matchPlayer => matchPlayer.PlayerId == userId)
            .Select(matchPlayer => new FootballMatch
            {
                Id = matchPlayer.FootballMatch.Id,
                PitchAddress = matchPlayer.FootballMatch.PitchAddress,
                MaxMatchPlayersNeed = matchPlayer.FootballMatch.MaxMatchPlayersNeed,
                PitchPrice = matchPlayer.FootballMatch.PitchPrice,
                Description = matchPlayer.FootballMatch.Description,
                StartTime = matchPlayer.FootballMatch.StartTime,
                Address = matchPlayer.FootballMatch.Address,
                CompetitionLevel = matchPlayer.FootballMatch.CompetitionLevel,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<User> GetUserByUsernameQueryAsync(
        string userName,
        CancellationToken cancellationToken)
    {
        return _users
            .Where(user => user.NormalizedUserName.Equals(userName.ToUpper()))
            .Select(user => new User
            {
                Id = user.Id
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}