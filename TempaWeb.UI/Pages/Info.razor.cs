using TempaWeb.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.UI.Pages
{
    public partial class Info
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public InfoMessageServices infoMessageServices { get; set; }



        public  void Navigate() {
            NavigationManager.NavigateTo(infoMessageServices.Route);
        }

    }
}
