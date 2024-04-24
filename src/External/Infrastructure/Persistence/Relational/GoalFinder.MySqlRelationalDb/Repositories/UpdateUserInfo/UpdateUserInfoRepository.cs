using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.UpdateUserInfo;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo
{
    internal sealed partial class UpdateUserInfoRepository : IUpdateUserInfoRepository
    {
        private readonly GoalFinderContext _context;
        private readonly DbSet<UserDetail> _userDetail;
        private readonly DbSet<User> _user;


        internal UpdateUserInfoRepository(GoalFinderContext context)
        {
            _context = context;
            _userDetail = _context.Set<UserDetail>();
            _user = _context.Set<User>();
        }
    }
}
