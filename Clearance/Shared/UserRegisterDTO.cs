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
        [Required(ErrorMessage ="الرجاء إدخال اسم الموظف")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال كنية الموظف")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال اسم الأب")]
        public string Father { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال الإيميل")]
        [EmailAddress(ErrorMessage = "الرجاء إدخال إيميل صحيح")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال كلمة المرور")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
        public string? ConfirmPassword { get; set; }
        public int? Direction_Id { get; set; }
    }
}
