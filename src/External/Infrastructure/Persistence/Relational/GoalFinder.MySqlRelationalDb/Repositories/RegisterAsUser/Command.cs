using GoalFinder.Data.Entities;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;

internal partial class RegisterAsUserRepository
{
    public async Task<bool> CreateAndAddUserToRoleCommandAsync(
        User newUser,
        string userPassword,
        UserManager<User> userManager,
        CancellationToken cancellationToken)
    {
        var executedTransactionResult = false;

        await _context.Database
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken);

                try
                {
                    var result = await userManager.CreateAsync(
                        user: newUser,
                        password: userPassword);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    result = await userManager.AddToRoleAsync(
                        user: newUser,
                        role: "user");

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
