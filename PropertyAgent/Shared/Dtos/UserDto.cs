using PropertyAgent.Shared.Enums;

namespace PropertyAgent.Dtos.Shared
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string KnownAs { get; set; }
        public Gender Gender { get; set; }
    }
}
