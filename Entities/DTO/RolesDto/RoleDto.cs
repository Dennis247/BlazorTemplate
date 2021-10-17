using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO.RolesDto
{
   public class AddRoleDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class EditRoleDto
    {
        [Required]
        public string RoleId { get; set; }
        public string Name { get; set; }
    }

    public class DeleteRoleDto
    {
        [Required]
        public string RoleId { get; set; }
    }


    public class RoleByNameDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class AddUserToRoleDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<string> Roles { get; set; }
    }

    public class GetUserRoleResultDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<IdentityRoleDto> Roles { get; set; }
    }

    public class IdentityRoleDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class GetAllUserRolesDto
    {
        [Required]
        public string UserId { get; set; }
    }
}
