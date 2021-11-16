using AutoMapper;
using TempaWeb.api.Context;
using TempaWeb.api.TokenHelpers;
using EmailService;
using Entities.DTO;
using Entities.DTO.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize("users.view")]
    public class UsersController :  ControllerBase

    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public UsersController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users =  _userManager.Users.ToList();
            List<UserDto> usersToReturn = new List<UserDto>();
            _mapper.Map(users, usersToReturn);
            return Ok(
                   new ApiResponse<List<UserDto>>
                   {
                       IsSucessFull = true,
                       Message = "Users List Fetched Sucessful",
                       Payload = usersToReturn
                   });
        }


        [HttpPost("GetUsersById")]
        public IActionResult GetUsersById([FromBody]GetUserByIdDto getUserByIdDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var user = _userManager.Users.FirstOrDefault(x=>x.Id == getUserByIdDto.Id);
            return Ok(
                   new ApiResponse<User>
                   {
                       IsSucessFull = true,
                       Message = "Users Fetched Sucessful",
                       Payload = user
                   });
        }


    }
}
