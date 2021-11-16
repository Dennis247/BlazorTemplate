using TempaWeb.Repository.Auth;
using TempaWeb.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.UI.Pages.Accounts
{
    public partial class Login
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public InfoMessageServices infoMessageServices { get; set; }

        private UserForAuthenticationDto _userForAuthenticationDto { get; set; }

        public string ErrorMessage { get; set; } = "";

        private bool isLoading { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _userForAuthenticationDto = new UserForAuthenticationDto();
        }

        public async Task LoginUser()
        {
            isLoading = true;
            var result = await AuthenticationService.Login(_userForAuthenticationDto);
            if (!result.IsSucessFull)
            {
                //Todo
                isLoading = false;
                ErrorMessage = result.Message;
           

                //check for diffrent login state
            }
            else
            {
                isLoading = false;
                NavigationManager.NavigateTo("/dashboard");
            }
        }
    }
}
