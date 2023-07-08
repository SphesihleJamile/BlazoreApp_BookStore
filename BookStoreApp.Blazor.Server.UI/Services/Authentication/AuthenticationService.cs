using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using BookStoreApp.Blazor.Server.UI.Services.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthClient _httpAuthClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IAuthClient httpAuthClient,
                                    ILocalStorageService localStorage,
                                    AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpAuthClient = httpAuthClient;
            this._localStorage = localStorage;
            this._authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(UserLoginVM loginModel)
        {
            var response = await _httpAuthClient.LoginAsync(loginModel);

            //Store Token
            await _localStorage.SetItemAsync("AccessToken", response.Token);

            //Change auth state of app
            await ((APIAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((APIAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}
