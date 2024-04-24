using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo
{
    internal partial class UpdateUserInfoRepository
    {
        public async Task<bool> UpdateUserCommandAsync(
            UserDetail updateUser,
            UserManager<UserDetail> userManager,
            CancellationToken cancellationToken)
        {
            var updateTransactionResult = false;

            await _context.Database
                .CreateExecutionStrategy()
                .ExecuteAsync(operation: async () =>
                {
                    await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                        cancellationToken: cancellationToken);

                    try
                    {
                        _context.Entry(updateUser).State = EntityState.Modified;

                        await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                        await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                        updateTransactionResult = true;
                    }
                    catch (Exception ex)
                    {
                        await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);

                    }
                });

            return updateTransactionResult;
        }
    }
}
