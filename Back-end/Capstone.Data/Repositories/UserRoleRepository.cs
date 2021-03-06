﻿using Capstone.Data.Infrastructrure;
using Capstone.Model;
using System;
using System.Linq;

namespace Capstone.Data.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        UserRole CheckExist(string UserID, Guid RoleID);
    }

    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public UserRole CheckExist(string UserID, Guid RoleID)
        {
            return DbContext.UserRoles.Where(u => u.UserID.Equals(UserID) && u.RoleID == RoleID).FirstOrDefault();
        }
    }
}
