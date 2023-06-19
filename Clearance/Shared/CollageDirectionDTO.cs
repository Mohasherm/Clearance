using System.ComponentModel.DataAnnotations;

namespace Clearance.Shared
{
    public class CollageDirectionDTO
    {
        public int Id { get; set; }


        public int CollageId { get; set; }
        
        
        [Required]
        public int DirectionId { get; set; }

        public string DirectionName { get; set; } = string.Empty;
    }
}
