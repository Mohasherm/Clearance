using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال اسم القسم")]

        public string Name { get; set; }
        [Required]
        public int CollageId { get; set; }
    }
}
