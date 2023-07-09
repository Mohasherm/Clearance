using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CollageId { get; set; }
        [ForeignKey(nameof(CollageId))]
        public Collage Collage { get; set; }
    }
}
