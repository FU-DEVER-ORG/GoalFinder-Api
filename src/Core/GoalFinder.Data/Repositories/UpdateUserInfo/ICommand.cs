using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.UpdateUserInfo
{
    public partial interface IUpdateUserInfoRepository
    {
        Task<bool> UpdateUserCommandAsync(
            UserDetail updateUser,
            UserManager<UserDetail> userManager,
            CancellationToken cancellationToken);
    }
}
