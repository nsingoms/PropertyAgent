using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyAgent.Shared.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
