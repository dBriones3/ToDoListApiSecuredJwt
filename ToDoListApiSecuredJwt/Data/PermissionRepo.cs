using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public class PermissionRepo : IPermissionRepo
    {
        public readonly ApiDbContext _context;
        public PermissionRepo(ApiDbContext context)
        {
            _context = context;
        }
        public void CreatePermissionXUserRelationship(List<PermissionXUser> permissionXUser)
        {
            _context.AddRange(permissionXUser);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
