using PropertyAgent.Shared.Enums;

namespace PropertyAgent.Shared.Dtos
{
    public class EmployeeUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; } 
        public Gender Gender { get; set; }
        public string ProfilePic { get; set; }
    }
}
