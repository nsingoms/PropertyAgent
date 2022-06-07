using PropertyAgent.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace PropertyAgent.Shared.Dtos
{
    public class RegisterDto : LoginDto
    {
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = default!;
      
    }
}
