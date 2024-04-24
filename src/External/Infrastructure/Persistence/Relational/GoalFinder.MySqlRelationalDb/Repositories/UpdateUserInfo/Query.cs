using GoalFinder.Data.Entities;
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
        public Task<bool> IsUserFoundByUserNameQueryAsync(
            string userName, 
            CancellationToken cancellationToken) 
        { 
            userName = userName.ToUpper();
            return _context
                .Set<User>()
                .AnyAsync(
                    predicate: user =>
                        user.NormalizedUserName.Equals(userName),
                    cancellationToken: cancellationToken);
        }
    }
}
