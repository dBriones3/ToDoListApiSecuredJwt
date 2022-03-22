using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;
using static ToDoListApiSecuredJwt.Constants.PermissionClaimStringConst;

namespace ToDoListApiSecuredJwt.Data
{
    public static class DataDbSeeder
    {
        public static void DbPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApiDbContext>());
            }
        }

        public static void SeedData(ApiDbContext context)
        {

            if(!context.Permissions.Any())
            {
                context.Permissions.AddRange(
                        new Permission { Permit = TodoListCreate},
                        new Permission { Permit = TodoListUpdate},
                        new Permission { Permit = TodoListDelete}
                    );

                context.SaveChanges();
            }
        }
    }
}
