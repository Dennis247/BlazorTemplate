using BlazorTemplate.api.Context;
using Entities.DTO;
using Entities.DTO.RolesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesManagementController : ControllerBase
    {
        private readonly ILogger<RolesManagementController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesManagementController(ILogger<RolesManagementController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet("GetRoles")]
        public  IActionResult GetRoles()
        {
            List<IdentityRole> result =  _roleManager.Roles.ToList();
            return Ok(new ApiResponse<List<IdentityRole>> { IsSucessFull = true, Message = "Role Creation Failed", Payload = result });
        }


        [HttpGet("GetRoleByName")]
        public async Task<IActionResult> GetRoleByName(RoleByNameDto roleByNameDto)
        {
            IdentityRole result = await _roleManager.FindByNameAsync(roleByNameDto.Name);
            return Ok(new ApiResponse<IdentityRole> { IsSucessFull = true, Message = "Role Creation Failed", Payload = result });
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleDto addRoleDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var result = await _roleManager.CreateAsync(new IdentityRole(addRoleDto.Name.Trim()));
            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Role Creation Sucessful", Payload = null });
            }
            return Ok(new ApiResponse<IEnumerable<String>> { IsSucessFull = false, Message = "Role Creation Failed", Payload = result.Errors.Select(e=>e.Description) });
        }


        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole(EditRoleDto editRoleDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });


            var existingRole  = await _roleManager.FindByIdAsync(editRoleDto.RoleId);
            if(existingRole == null)
            {
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Role Not Found",
                    Payload = null
                });
            }
            existingRole.Name = editRoleDto.Name;

            var result = await _roleManager.UpdateAsync(existingRole);

            if(result.Succeeded)
            {
                return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Role Update Sucessful", Payload = null });
            }
            return Ok(new ApiResponse<IEnumerable<String>> { IsSucessFull = true, Message = "Role Update Failed", Payload = result.Errors.Select(e => e.Description) });
        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRole(DeleteRoleDto deleteRoleDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });


            var existingRole = await _roleManager.FindByIdAsync(deleteRoleDto.RoleId);
            if (existingRole == null)
            {
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Role Not Found",
                    Payload = null
                });
            }

            //deletes roles & claims assigned to roles
            var result = await _roleManager.DeleteAsync(existingRole);

            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Delete Sucessful", Payload = null });
            }
            return Ok(new ApiResponse<IEnumerable<String>> { IsSucessFull = true, Message = "Delete Failed", Payload = result.Errors.Select(e => e.Description) });
        }

    }
}
