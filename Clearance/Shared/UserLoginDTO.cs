using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage ="الرجاء إدخال الإيميل")]
        [EmailAddress(ErrorMessage = "أدخل إيميل صحيح")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال كلمة المرور")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
