using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Helpers.PermissionHelpers
{
    public static class RolesClaims
    {
        public const string Add = "roles.add";
        public const string Edit = "roles.edit";
        public const string Delete = "roles.delete";
        public const string View = "roles.view";


        public static List<string> ClaimsList = new List<string>{Add, Edit, Delete, View};
    }

    public static class UsersClaims
    {
        public const string Add = "users.add";
        public const string Edit = "users.edit";
        public const string Delete = "users.delete";
        public const string View = "users.view";


        public static List<string> ClaimsList = new List<string> { Add, Edit, Delete, View };
    }

    public static class ClaimsList
    {
        public static  List<string> AllClaimsList
        {
            get
            {
                return RolesClaims.ClaimsList;
            }
        }
    }


}
