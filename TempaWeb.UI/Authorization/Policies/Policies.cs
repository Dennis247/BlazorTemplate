using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.Authorization.Policies
{
    public static class Policies
    {
     
        public const string IsUser = "IsUser";

        public static AuthorizationPolicy IsUsersPolicy()
        {
            return new AuthorizationPolicyBuilder()
                                                   .RequireClaim("permission",new List<string>{ "users.view"})
                                                   .Build();
        }

     
    }
}
