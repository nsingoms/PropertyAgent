namespace PropertyAgent.Client.Interfaces
{
    public interface IAuthServices
    {
        Task<RegistrationResponseDto> RegisterUserAsync(RegisterDto registerDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task Logout();
    }
}
