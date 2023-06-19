using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class DirectionDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "يرجى إدخال اسم الجهة")]
        public string Name { get; set; }
    }
}
