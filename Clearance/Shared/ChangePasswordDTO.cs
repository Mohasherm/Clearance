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


        [Required(ErrorMessage = "يرجى إدخال كلمة المرور الحالية")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;


        [Required(ErrorMessage = "يرجى إدخال كلمة المرور الجديدة")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;


        [Required(ErrorMessage = "يرجى إدخال تأكيد كلمة المرور الجديدة")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور الجديدة غير متطابقة")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
