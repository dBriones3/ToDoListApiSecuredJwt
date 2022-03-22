using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApiSecuredJwt.Constants
{
    public static class PermissionClaimStringConst
    {
        public static string TodoListCreate = "todoList.create";
        public static string TodoListUpdate = "todoList.update";
        public static string TodoListDelete = "todoList.delete";
    }
}
