using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile
{
    internal sealed partial class GetUserProfileRepository
    {

        public Task<bool> IsUserTemporarilyRemovedQueryAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return _userDetails
                .Where(predicate:
                    userDetail => userDetail.UserId == userId &&
                    userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
                    userDetail.RemovedAt != DateTime.MinValue)
                .AnyAsync(cancellationToken: cancellationToken);
        }
        public async Task<UserDetail> GetUserDetailAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _userDetails
            .AsNoTracking()
            .Where(userdetail => userdetail.UserId == userId)
            .Include(userdetail => userdetail.UserPositions)
            .ThenInclude(userPositions => userPositions.Position.FullName)
            .Select(userdetail => new UserDetail
            {
                FirstName = userdetail.FirstName,
                LastName = userdetail.LastName,
                Description = userdetail.Description,
                PrestigeScore = userdetail.PrestigeScore,
                Address = userdetail.Address,
                AvatarUrl = userdetail.AvatarUrl,
                Experience = userdetail.Experience,
                CompetitionLevel = userdetail.CompetitionLevel,
                UserPositions = userdetail.UserPositions
            }).FirstAsync();
        }
        public async Task<IEnumerable<FootballMatch>> GetFootballMatchByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _matchPlayer
            .AsNoTracking()
            .Where(matchPlayer => matchPlayer.PlayerId == userId)
            .Select(matchPlayer => matchPlayer.FootballMatch) // Include
            .Select(match => new FootballMatch
            {
                Id = match.Id,
                PitchAddress = match.PitchAddress,
                MaxMatchPlayersNeed = match.MaxMatchPlayersNeed,
                PitchPrice = match.PitchPrice,
                Description = match.Description,
                StartTime = match.StartTime,
                Address = match.Address,
                CompetitionLevel = match.CompetitionLevel,
            }).ToListAsync();
        }

        

    }
}