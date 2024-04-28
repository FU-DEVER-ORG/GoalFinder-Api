using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.RegisterAsUser;

public partial interface IRegisterAsUserRepository
{
    Task<bool> IsUserFoundByNormalizedEmailOrUsernameQueryAsync(
        string userEmail,
        CancellationToken cancellationToken
    );
}
