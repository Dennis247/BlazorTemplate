using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO.PermissionDto
{
   public class RoleClaminsDto
    {
       [Required]
      public string RoleId { get; set; } 
      [Required]
      public   List<string> ClaimsTypes { get; set; }
    }


    public class GetRoleClaimsDto
    {
        [Required]
        public string RoleId { get; set; }
    }


    public class UserPermissionDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<string> Claims { get; set; }
    }


    public class GetAllUserPermissionDto
    {
        [Required]
        public string UserId { get; set; }
    }


}
