using Entities.DTO;
using Entities.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.HttpRepository.Users
{
   public interface IUsersService
    {
        Task<ApiResponse<List<UserDto>>> GetUsers();
    }
}
