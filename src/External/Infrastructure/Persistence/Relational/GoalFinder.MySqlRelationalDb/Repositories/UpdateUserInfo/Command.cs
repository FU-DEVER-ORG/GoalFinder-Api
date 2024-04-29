using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo;

internal partial class UpdateUserInfoRepository
{
    public async Task<bool> UpdateUserCommandAsync(
        UserDetail updateUser,
        UserDetail currentUser,
        IEnumerable<Guid> currentPositionIds,
        IEnumerable<Guid> newPositionIds,
        CancellationToken cancellationToken
    )
    {
        var updateTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                // User table database operation.
                #region UserTable
                if (!currentUser.User.UserName.Equals(updateUser.User.UserName))
                {
                    User user =
                        new()
                        {
                            Id = updateUser.UserId,
                            UserName = updateUser.User.UserName,
                            ConcurrencyStamp = currentUser.User.ConcurrencyStamp
                        };

                    var entry = _users.Entry(entity: user);

                    entry.State = EntityState.Unchanged;

                    entry.Property(propertyExpression: entry => entry.UserName).IsModified = true;
                }
                #endregion

                // User position table database operation.
                #region UserPositiontable
                var removedPositionIds = currentPositionIds.Except(second: newPositionIds);

                if (!removedPositionIds.IsNullOrEmpty())
                {
                    await _userPositions
                        .Where(userPosition =>
                            userPosition.UserId == currentUser.UserId
                            && removedPositionIds.Contains(userPosition.PositionId)
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);
                }

                var addedPositions = newPositionIds
                    .Except(second: currentPositionIds)
                    .Select(newPositionId => new UserPosition
                    {
                        UserId = currentUser.UserId,
                        PositionId = newPositionId
                    });

                await _userPositions.AddRangeAsync(
                    entities: addedPositions,
                    cancellationToken: cancellationToken
                );
                #endregion

                // User detail table database operation.
                #region UserDetailTable
                var updateUserEntry = _userDetails.Entry(entity: updateUser);
                var currentUserEntry = _userDetails.Entry(entity: currentUser);

                updateUserEntry.State = EntityState.Unchanged;

                foreach (var property in updateUserEntry.Properties)
                {
                    if (
                        !property.Metadata.IsPrimaryKey()
                        && !Equals(
                            objA: property.CurrentValue,
                            objB: currentUserEntry
                                .Property(propertyName: property.Metadata.Name)
                                .CurrentValue
                        )
                    )
                    {
                        property.IsModified = true;
                    }
                }
                #endregion

                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    updateTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return updateTransactionResult;
    }
}
