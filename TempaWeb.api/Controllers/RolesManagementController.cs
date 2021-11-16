using TempaWeb.api.Context;
using TempaWeb.api.Helpers.Filters;
using Entities.DTO;
using Entities.DTO.RolesDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.api.Controllers
{
   // [Authorize(Roles = "Administrator")]
    [Authorize(Policy ="roles.add")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesManagementController : ControllerBase
    {
        private readonly ILogger<RolesManagementController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;


        public RolesManagementController(ILogger<RolesManagementController> logger,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("GetRoles")]
        public  IActionResult GetRoles()
        {
            List<IdentityRole> result =  _roleManager.Roles.ToList();
            return Ok(new ApiResponse<List<IdentityRole>> { IsSucessFull = true, Message = "Fetch Roles Sucessful", Payload = result });
        }


        [HttpGet("GetRoleByName")]
        public async Task<IActionResult> GetRoleByName(RoleByNameDto roleByNameDto)
        {
            IdentityRole result = await _roleManager.FindByNameAsync(roleByNameDto.Name);
            return Ok(new ApiResponse<IdentityRole> { IsSucessFull = true, Message = "Fetch Role Sucessful", Payload = result });
        }


     
        [HttpPost("AddRole")]
        [AuditTrailFilter]
        public async Task<IActionResult> AddRole(AddRoleDto addRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
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

            return BadRequest(new ApiResponse<IEnumerable<String>> { IsSucessFull = false, Message = "Role Creation Failed", Payload = result.Errors.Select(e=>e.Description) });
        }


        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleDto addUserToRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var existingUser = await _userManager.FindByIdAsync(addUserToRoleDto.UserId);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "User Not Found",
                    Payload = null
                });
            }


            var result = await _userManager.AddToRolesAsync(existingUser, addUserToRoleDto.Roles);
            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "User added to Roles Sucessful", Payload = null });
            }
            return BadRequest(
                new ApiResponse<IEnumerable<String>>
                {
                    IsSucessFull = false, 
                    Message = "User Added to Roles Failed",
                    Payload = result.Errors.Select(e => e.Description)
                });
        }


        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole(EditRoleDto editRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });


            var existingRole  = await _roleManager.FindByIdAsync(editRoleDto.RoleId);
            if(existingRole == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
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

            return BadRequest(new ApiResponse<IEnumerable<String>> { 
                IsSucessFull = true,
                Message = "Role Update Failed",
                Payload = result.Errors.Select(e => e.Description) 
            });
        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRole(DeleteRoleDto deleteRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });


            var existingRole = await _roleManager.FindByIdAsync(deleteRoleDto.RoleId);
            if (existingRole == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
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
                return Ok(
                    new ApiResponse<string> { 
                        IsSucessFull = true,
                        Message = "Delete Sucessful",
                        Payload = null 
                    });
            }
            return BadRequest(new ApiResponse<IEnumerable<String>> { IsSucessFull = true, Message = "Delete Failed", Payload = result.Errors.Select(e => e.Description) });
        }

        [HttpPost("GetAllUserRoles")]
        public async Task<IActionResult> GetAllUserRoles(GetAllUserRolesDto getAllUserRolesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });



            var existingUser = await _userManager.FindByIdAsync(getAllUserRolesDto.UserId);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "User Not Found",
                    Payload = null
                });
            }

            var userRoles = await _userManager.GetRolesAsync(existingUser);

            var identityRoles = new List<IdentityRoleDto>();
            
            foreach (var item in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(item);
                identityRoles.Add(new IdentityRoleDto { RoleId = role.Id, RoleName = role.Name });
            }

            return Ok(new ApiResponse<GetUserRoleResultDto>
            {
                IsSucessFull = true,
                Message = "Users Roles fetched Sucessful",
                Payload = new GetUserRoleResultDto { UserId = existingUser.Id,Roles = identityRoles}
            });

        }

    }
}
