using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class CollageDirection
    {
        [Key]
        public int Id { get; set; }
        public int CollageId { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey(nameof(CollageId))]
        public Collage Collage { get; set; }
        [ForeignKey(nameof(DirectionId))]
        public Direction Direction { get; set; }
    }
}
