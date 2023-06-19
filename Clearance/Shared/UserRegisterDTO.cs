using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "يرجى إدخال الاسم")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "يرجى إدخال الكنية")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "يرجى إدخال اسم الأب")]
        public string Father { get; set; } = string.Empty;


        [Required(ErrorMessage = "يرجى إدخال البريد الإلكتروني")]
        [EmailAddress]
        public string? Email { get; set; }


        [Required(ErrorMessage = "يرجى إدخال كلمة المرور")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [Required(ErrorMessage = "يرجى إدخال تأكيد كلمة المرور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password not match")]
        public string? ConfirmPassword { get; set; }


        public int? Direction_Id { get; set; }
    }
}
