using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public interface IPermissionRepo
    {
        bool SaveChanges();
        void CreatePermissionXUserRelationship(List<PermissionXUser> permissionXUser);
    }
}
