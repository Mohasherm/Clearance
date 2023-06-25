using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class ClearanceDirection
    {
        [Key]
        public int Id { get; set; }
        public int ClearanceId { get; set; }
        public int DirectionId { get; set; }
        public bool? State { get; set; }

        [ForeignKey(nameof(ClearanceId))]
        public Clearance Clearance{ get; set; }

        [ForeignKey(nameof(DirectionId))]
        public Direction Direction { get; set; }
    }
}
