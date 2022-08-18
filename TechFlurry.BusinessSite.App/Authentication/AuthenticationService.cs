using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TechFlurry.BusinessSite.App.Custom;

namespace TechFlurry.BusinessSite.App.Authentication
{
    internal interface IAuthenticationService
    {
        Task<bool> Login( string token );
        Task Logout();
    }

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService( AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage )
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> Login( string token )
        {
            var task = _localStorage.SetItemAsync(Constants.TOKEN_NAME, token).AsTask();
            await task;
            if (task.Status == TaskStatus.RanToCompletion)
            {
                ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(token);
                return true;
            }
            return false;
        }


        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(Constants.TOKEN_NAME);
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
        }
    }
}
