using System.ComponentModel.DataAnnotations;

namespace Clearance.Shared
{
    public class CollageDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
    }
}
