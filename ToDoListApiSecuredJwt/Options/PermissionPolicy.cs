using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ToDoListApiSecuredJwt.Constants.PermissionConst;
using static ToDoListApiSecuredJwt.Constants.PermissionClaimStringConst;

namespace ToDoListApiSecuredJwt.Options
{
    public static class PermissionPolicy
    {
        public static IServiceCollection AddAuthPolicies(this IServiceCollection services)
        {
            return services.AddAuthorization(opt => 
            {
                opt.AddPolicy(AllowCreateItem, policy => policy.RequireClaim(TodoListCreate));
                opt.AddPolicy(AllowUpdateItem, policy => policy.RequireClaim(TodoListUpdate));
                opt.AddPolicy(AllowDeleteItem, policy => policy.RequireClaim(TodoListDelete));
            });
        }
    }
}
