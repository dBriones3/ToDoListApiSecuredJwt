using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        void CreateUser(User user);
        User GetUserByEmail(string email);
    }
}
