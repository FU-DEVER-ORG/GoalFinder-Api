using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.MySqlRelationalDb.Repositories.Login;

internal partial class LoginRepository
{
    public async Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _context
                .Set<RefreshToken>()
                .AddAsync(entity: refreshToken, cancellationToken: cancellationToken);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
