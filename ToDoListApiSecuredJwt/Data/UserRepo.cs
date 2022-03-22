using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly ApiDbContext _context;

        public UserRepo(ApiDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if (user == null) throw new ArgumentException(nameof(user));

            _context.Add(user);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users
                .Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                .Include(x => x.PermissionsXUser)
                .ThenInclude(x => x.Permit)
                .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
