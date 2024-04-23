using System.Threading.Tasks;
using System.Threading;
using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GoalFinder.Data.Repositories.RegisterAsUser;

public partial interface IRegisterAsUserRepository
{
    Task<bool> CreateAndAddUserToRoleCommandAsync(
        User newUser,
        string userPassword,
        UserManager<User> userManager,
        CancellationToken cancellationToken);
}
