namespace PropertyAgent.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(HttpClient httpClient,ILocalStorageService localStorage)
        {
            this._httpClient = httpClient;
            this._localStorage = localStorage;
            //Initialize empty token
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //get token from local storage 
            var token = await _localStorage.GetItemAsync<string>("authToken");

            //if token is empty or null then the user is not Authenticated
            if(string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }
            //set the default Authorization header for the httpClient
            _httpClient.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("bearer",token);

            //Populate the claims identity constructor with with parsed claims and the authentication type parameters
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        }
        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(
                                          new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
          
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
