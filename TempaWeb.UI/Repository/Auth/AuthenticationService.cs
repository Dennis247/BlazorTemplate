using Blazored.LocalStorage;
using TempaWeb.AuthProviders;
using Entities.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TempaWeb.UI.Helpers;
using System.Net;

namespace TempaWeb.Repository.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider; 
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpHelper _httHelper;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage, IHttpHelper httHelper)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider; 
            _localStorage = localStorage;
            _httHelper =  httHelper; 
        }

        public async Task<ApiResponse<string>> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var registrationResult = await _client.PostAsync("accounts/registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<string>>(registrationContent, _options);
            return result;
           
        }

        public async Task<ApiResponse<AuthResponseDto>> Login(UserForAuthenticationDto userForAuthentication)
        {
            var loginResponse = await _httHelper.Post("accounts/login", userForAuthentication);
            var result = JsonSerializer.Deserialize<ApiResponse<AuthResponseDto>>(loginResponse.data, _options);
            if (result.IsSucessFull )
            {
                await _localStorage.SetItemAsync("authToken", result.Payload.Token);
                await _localStorage.SetItemAsync("refreshToken", result.Payload.RefreshToken);
                _httHelper.setBearerToken(result.Payload.Token);
                ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Payload.Token);
            }
            return result;

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

            var tokenDto = JsonSerializer.Serialize(new RefreshTokenDto { Token = token, RefreshToken = refreshToken });
            var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");

            var refreshResult = await _client.PostAsync("token/refresh", bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(refreshContent, _options);

            if (!refreshResult.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong during the refresh token action");

            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result.Token;
        }
    }
}
