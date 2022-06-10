using PropertyAgent.Shared.Enums;
using System.Text.Json.Serialization;

namespace PropertyAgent.Shared.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string KnownAs { get; set; } = default!;
        public Gender Gender { get; set; }
        public string ProfilePic { get; set; } = default!;
        
        public virtual List<PropertyDto> PropertyDtos { get; set; } = new();
    }
}
