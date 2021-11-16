using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.Repository.Auth
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<ApiResponse<AuthResponseDto>> Login(UserForAuthenticationDto userForAuthentication); 
        Task Logout();

        Task<string> RefreshToken();
    }
}
