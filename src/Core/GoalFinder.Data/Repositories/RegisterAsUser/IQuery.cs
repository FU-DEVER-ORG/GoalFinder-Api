using System.Threading.Tasks;
using System.Threading;

namespace GoalFinder.Data.Repositories.RegisterAsUser;

public partial interface IRegisterAsUserRepository
{
    Task<bool> IsUserFoundByNormalizedEmailOrUsernameQueryAsync(
        string userEmail,
        CancellationToken cancellationToken);
}
