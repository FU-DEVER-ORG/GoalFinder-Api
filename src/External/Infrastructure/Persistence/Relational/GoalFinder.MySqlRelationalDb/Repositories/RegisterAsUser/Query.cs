using System.Threading.Tasks;
using System.Threading;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;

internal partial class RegisterAsUserRepository
{
    public Task<bool> IsUserFoundByNormalizedEmailOrUsernameQueryAsync(
        string userEmail,
        CancellationToken cancellationToken)
    {
        userEmail = userEmail.ToUpper();

        return _context
            .Set<User>()
            .AnyAsync(
                predicate: user =>
                    user.NormalizedEmail.Equals(userEmail) &&
                    user.NormalizedUserName.Equals(userEmail),
                cancellationToken: cancellationToken);
    }
}
