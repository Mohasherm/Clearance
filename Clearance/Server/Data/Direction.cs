using System.ComponentModel.DataAnnotations;

namespace Clearance.Server.Data
{
    public class Direction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
