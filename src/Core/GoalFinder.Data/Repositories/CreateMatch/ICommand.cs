using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.CreateMatch;

/// <summary>
///     Interface for CreateMatchRepository
/// </summary>
public partial interface ICreateMatchRepository
{
    Task<bool> CreateMatchCommandAsync(
        Guid userId,
        FootballMatch footballMatch,
        CancellationToken cancellationToken
    );
}
