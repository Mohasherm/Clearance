using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class DepartmentDirection
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
        [ForeignKey(nameof(DirectionId))]
        public Direction Direction { get; set; }
    }
}
