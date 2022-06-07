namespace PropertyAgent.Client.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _stateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly JsonSerializerOptions _options;
        public AuthServices(HttpClient httpClient, 
            AuthenticationStateProvider stateProvider,
            ILocalStorageService localStorage)
        {
            this._httpClient = httpClient;
            this._stateProvider = stateProvider;
            this._localStorage = localStorage;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
          var content = JsonSerializer.Serialize(loginDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _httpClient.PostAsync("auth/login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            //if not successfull throw exception 
            if (authResult.IsSuccessStatusCode == false)
                return null;

            var result = JsonSerializer.Deserialize<UserDto>(authContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //if response is success Save token to the local Storage
            await _localStorage.SetItemAsync("authToken", result?.Token);

            //Call the Notify user method from the AuthState Provider
            ((AuthStateProvider)_stateProvider).NotifyUserAuthentication(result.Token);

            //Add the Token as the Default Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result;

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_stateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var content = JsonSerializer.Serialize(registerDto);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _httpClient.PostAsync("auth/register", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();

            if (registrationResult.IsSuccessStatusCode == false)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result;
            }
            return new RegistrationResponseDto { IsSuccessfulRegistration = true };

        }
    }
}
