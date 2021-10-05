using BlazorTemplate.HttpRepository.Users;
using Entities.DTO.UserDto;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.Pages.Users
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUsersService UsersService { get; set; }

        private List<UserDto> Users = new List<UserDto>();

        private bool isLoading { get; set; }

        protected async override Task OnInitializedAsync()
        {
            //get list of users
            await LoadUsers();
        }

        private async Task  LoadUsers()
        {
            isLoading = true;
            var result = await UsersService.GetUsers();
            if (!result.IsSucessFull)
            {
                //show error notification
            }
            else
            {
                Users = result.Payload;
            }
            isLoading = false;


        }
    }
}
