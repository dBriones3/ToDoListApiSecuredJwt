using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApiSecuredJwt.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Permit { get; set; }

        public List<PermissionXUser> PermissionsXUser { get; set; }
    }
}
