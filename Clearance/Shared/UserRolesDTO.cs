using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class UserRolesDTO
    {
        public Guid User_Id { get; set; }

        [Required(ErrorMessage ="الرجاء إختيار اسم الصلاحية")]

        public string RoleName { get; set; }
    }
}
