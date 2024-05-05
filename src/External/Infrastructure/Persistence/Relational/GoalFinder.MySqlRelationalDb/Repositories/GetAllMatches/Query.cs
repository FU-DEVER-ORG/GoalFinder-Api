using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllMatches;

/// <summary>
///     Implementation of <see cref="IGetAllMatchesQuery"/>
/// </summary>
internal sealed partial class GetAllMatchesRepository
{
    public async Task<IEnumerable<FootballMatch>> GetAllMatchesQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _footballMatch
            .AsNoTracking()
            .Where(predicate: match =>
                match.RemovedBy == CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && match.RemovedAt == DateTime.MinValue
            )
            .OrderBy(keySelector: match => match.CreatedAt)
            .Select(match => new FootballMatch()
            {
                Id = match.Id,
                PitchAddress = match.PitchAddress,
                MaxMatchPlayersNeed = match.MaxMatchPlayersNeed,
                PitchPrice = match.PitchPrice,
                Title = match.Title,
                Description = match.Description,
                MinPrestigeScore = match.MinPrestigeScore,
                StartTime = match.StartTime,
                Address = match.Address,
                CompetitionLevel = new CompetitionLevel
                {
                    FullName = match.CompetitionLevel.FullName,
                },
                CreatedAt = match.CreatedAt,
                HostId = match.HostId,
                UserDetail = new UserDetail
                {
                    FirstName = match.UserDetail.FirstName,
                    LastName = match.UserDetail.LastName,
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
