using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
