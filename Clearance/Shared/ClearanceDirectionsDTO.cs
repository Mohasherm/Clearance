using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class ClearanceDirectionsDTO
    {
        public int Id { get; set; }
        public int ClearanceId { get; set; }
        public int DirectionId { get; set; }
        public bool? State { get; set; }

        public string CollageName { get; set; } = string.Empty;
        public string DirectionName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string UnivNum { get; set; }
        public string NationalNum { get; set; }
        public string Department { get; set; } = string.Empty;
        public DateTime OrderApplyDate { get; set; }
    }
}
