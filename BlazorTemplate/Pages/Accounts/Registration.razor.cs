using BlazorTemplate.HttpRepository.Auth;
using BlazorTemplate.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.Pages.Accounts
{
    public partial class Registration
    {
        private UserForRegistrationDto _userForRegistration = new UserForRegistrationDto();


        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public InfoMessageServices infoMessageServices { get; set; }


       // public bool ShowRegistrationErros { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorResults { get; set; } = "";
        public async Task Register()
        {

            var result = await AuthenticationService.RegisterUser(_userForRegistration);
            if (!result.IsSucessFull)
            {
                ErrorMessage = result.Message;
                ErrorResults = result.Payload;
            }
            else
            {
                infoMessageServices.Message = result.Message;
                infoMessageServices.Title = "Registration Sucessful";
                infoMessageServices.Route = "/Login";
                NavigationManager.NavigateTo("/Info");
            }
        }

    }
}
