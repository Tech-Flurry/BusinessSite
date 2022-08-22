using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TechFlurry.BusinessSite.App.Custom;

namespace TechFlurry.BusinessSite.App.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        private readonly AuthenticationOptions _options;

        public AuthStateProvider( ILocalStorageService localStorage, AuthenticationOptions options )
        {
            _localStorage = localStorage;
            _options = options;
            _anonymous = new AuthenticationState(new ClaimsPrincipal());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(Constants.TOKEN_NAME);
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }
            //_httpClient.AddHeader("bearer", token);
            NotifyUserAuthentication(token);
            return new AuthenticationState(GetAuthenticatedUser(token));
        }

        public void NotifyUserAuthentication( string token )
        {
            var authenticatedUser = GetAuthenticatedUser(token);
            Task<AuthenticationState> authenticationState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authenticationState);
        }

        public void NotifyUserLogout()
        {
            Task<AuthenticationState> authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }

        internal ClaimsPrincipal GetAuthenticatedUser( string token )
        {
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var utcNow = DateTime.UtcNow;
            var audience = claims?.GetAppId();
            //Checks the authentication registered app
            if (audience == _options.AppId)
            {
                // Checks the nbf field of the token
                var notValidBefore = claims?.Where(x => x.Type.Equals("nbf")).FirstOrDefault();
                if (notValidBefore is not null)
                {
                    DateTimeOffset datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(notValidBefore.Value));
                    if (datetime.UtcDateTime > utcNow)
                        return new ClaimsPrincipal();
                }
                // Checks the exp field of the token
                var expiry = claims?.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
                if (expiry is not null)
                {
                    // The exp field is in Unix time
                    DateTimeOffset datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));
                    if (datetime.UtcDateTime <= utcNow)
                        return new ClaimsPrincipal();
                }
                return new ClaimsPrincipal(new ClaimsIdentity(claims, Constants.AUTH_TYPE));
            }
            else
            {
                return new ClaimsPrincipal();
            }
        }
    }
}
