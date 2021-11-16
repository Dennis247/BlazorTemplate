using TempaWeb.api.Context;
using TempaWeb.api.Helpers.PermissionHelpers;
using Entities.DTO;
using Entities.DTO.PermissionDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TempaWeb.api.Controllers
{
    //   [Authorize("roles.add")]
    [AllowAnonymous]
    public class PermissionsManagementController : ControllerBase
    {
        private readonly ILogger<PermissionsManagementController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public PermissionsManagementController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager =   userManager;
    }


        [HttpGet("GetAllClaims")]
        public  IActionResult GetAllClaims()
        {
            List<string> claimsResult = new List<string>();
            claimsResult.AddRange(ClaimsList.AllClaimsList);

            return Ok(new ApiResponse<IEnumerable<string>>
            {
                IsSucessFull = true,
                Message = "Claims Fetch Sucessful",
                Payload = claimsResult
            });

        }


        [HttpPost("AddClaimsToRole")]
        public async Task<IActionResult> AddClaimsToRole(RoleClaminsDto addClaimsToRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var existingRole = await _roleManager.FindByIdAsync(addClaimsToRoleDto.RoleId);
            if (existingRole == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Role Not Found",
                    Payload = null
                });
            }

            var existingRoleClaims = await _roleManager.GetClaimsAsync(existingRole);

            foreach (var item in addClaimsToRoleDto.ClaimsTypes)
            {
                if (!existingRoleClaims.Select(x => x.Value).Contains(item))
                {
                    await _roleManager.AddClaimAsync(existingRole, new Claim(CustomClaimTypes.Permission, item));
                }
            }
            return Ok(new ApiResponse<IEnumerable<string>>
            {
                IsSucessFull = true,
                Message = "Claims Added To Role Sucessful",
                Payload = null
            });

        }


        [HttpPost("RemoveClaimsFromRole")]
        public async Task<IActionResult> RemoveClaimsFromRole(RoleClaminsDto removeClaimsFromRole)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var existingRole = await _roleManager.FindByIdAsync(removeClaimsFromRole.RoleId);
            if (existingRole == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Role Not Found",
                    Payload = null
                });
            }

            foreach (var item in removeClaimsFromRole.ClaimsTypes)
            {
                await _roleManager.RemoveClaimAsync(existingRole, new Claim(CustomClaimTypes.Permission, item));

            }
            return Ok(new ApiResponse<IEnumerable<string>>
            {
                IsSucessFull = true,
                Message = "Claims Removed From Role Sucessful",
                Payload = null
            });

        }


        [HttpPost("GetRoleClaims")]
        public async Task<IActionResult> GetRoleClaims(GetRoleClaimsDto getRoleClaimsDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var existingRole = await _roleManager.FindByIdAsync(getRoleClaimsDto.RoleId);
            if (existingRole == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Role Not Found",
                    Payload = null
                });
            }


            var claims =  await _roleManager.GetClaimsAsync(existingRole);


            return Ok(new ApiResponse<IEnumerable<Claim>>
            {
                IsSucessFull = true,
                Message = "Claims Fetch Sucessfully",
                Payload = claims
            });

        }

        [HttpPost("AssignUserToPermissions")]
        public async Task<IActionResult> AssignUserToPermissions(UserPermissionDto assignUserPermissionDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });



            var existingUser = await _userManager.FindByIdAsync(assignUserPermissionDto.UserId);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "User Not Found",
                    Payload = null
                });
            }

            List<Claim> userClaims = new List<Claim>();
            foreach (var item in assignUserPermissionDto.Claims)
            {
                // add valid claims only for this app
                if (ClaimsList.AllClaimsList.Contains(item))
                {
                    userClaims.Add(new Claim(CustomClaimTypes.Permission, item));
                }
             
            }


            await  _userManager.AddClaimsAsync(existingUser, userClaims);


            return Ok(new ApiResponse<IEnumerable<Claim>>
            {
                IsSucessFull = true,
                Message = "Claims Added to User Sucessfully",
                Payload = null
            });

        }


        [HttpPost("RemoveUserPermissions")]
        public async Task<IActionResult> RemoveUserPermissions(UserPermissionDto assignUserPermissionDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });



            var existingUser = await _userManager.FindByIdAsync(assignUserPermissionDto.UserId);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "User Not Found",
                    Payload = null
                });
            }

            var userClaims = new List<Claim>();
            foreach (var item in assignUserPermissionDto.Claims)
            {
                // add valid claims only for this app
                if (ClaimsList.AllClaimsList.Contains(item))
                {
                    userClaims.Add(new Claim(CustomClaimTypes.Permission, item));
                }
            }

            await _userManager.RemoveClaimsAsync(existingUser, userClaims);


            return Ok(new ApiResponse<IEnumerable<Claim>>
            {
                IsSucessFull = true,
                Message = "Claims  Removed From User Sucessfully",
                Payload = null
            });

        }



        [HttpPost("GetAllUserPermissions")]
        public async Task<IActionResult> GetAllUserPermissions(GetAllUserPermissionDto getAllUserPermissionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });



            var existingUser = await _userManager.FindByIdAsync(getAllUserPermissionDto.UserId);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "User Not Found",
                    Payload = null
                });
            }

            var userPermissions = await _userManager.GetClaimsAsync(existingUser);
            return Ok(new ApiResponse<IEnumerable<Claim>>
            {
                IsSucessFull = true,
                Message = "Claims  Removed From User Sucessfully",
                Payload = userPermissions
            });

        }

    }
}
