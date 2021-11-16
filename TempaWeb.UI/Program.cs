using Blazored.LocalStorage;
using TempaWeb.Authorization.Policies;
using TempaWeb.AuthProviders;
using TempaWeb.Repository;
using TempaWeb.Repository.Auth;
using TempaWeb.Repository.Users;
using TempaWeb.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using TempaWeb.UI;
using TempaWeb.UI.Helpers;

namespace TempaWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/api/")
            }.EnableIntercept(sp));


            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore(config =>
            {
                config.AddPolicy(Policies.IsUser, Policies.IsUsersPolicy());
      
            });
       //     builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<RefreshTokenService>();
            builder.Services.AddScoped<HttpInterceptorService>();
            builder.Services.AddSingleton<InfoMessageServices>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IHttpHelper, HttpHelper>();

            await builder.Build().RunAsync();
        }
    }
}
