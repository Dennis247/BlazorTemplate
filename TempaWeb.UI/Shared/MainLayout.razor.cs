using TempaWeb.Helpers;
using TempaWeb.Repository.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TempaWeb.UI.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        IJSRuntime js { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await js.InitializeInactivityTimer(DotNetObjectReference.Create(this));
        
        }

       


        [JSInvokable]
        public  async Task LogOut()
        {
            var authState = await AuthenticationState;
            if (authState.User.Identity.IsAuthenticated)
            {
                await AuthenticationService.Logout();
                NavigationManager.NavigateTo("/");
            }
         
        }

    
    }
}
