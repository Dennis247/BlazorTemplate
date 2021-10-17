using Entities.DTO;
using Entities.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorTemplate.Repository.Users
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public UsersService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ApiResponse<List<UserDto>>> GetUsers()
        {
            try
            {
                var usersResult = await _client.GetAsync("users/GetUsers");
                var registrationContent = await usersResult.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<UserDto>>>(registrationContent, _options);
                return result;
            }
            catch (Exception ex)
            {

                throw ;
            }
     
        }
    }
}
