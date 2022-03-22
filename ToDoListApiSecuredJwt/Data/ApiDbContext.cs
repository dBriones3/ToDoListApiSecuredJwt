using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionXUser>()
                .HasOne(u => u.User)
                .WithMany(pxu => pxu.PermissionsXUser)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<PermissionXUser>()
                .HasOne(u => u.Permit)
                .WithMany(pxu => pxu.PermissionsXUser)
                .HasForeignKey(f => f.PermissionId);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PermissionXUser> PermissionXUser { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
