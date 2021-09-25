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
}
