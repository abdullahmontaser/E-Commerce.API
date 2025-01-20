using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email IS Required")]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; }
    }
}
