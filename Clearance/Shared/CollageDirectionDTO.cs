using System.ComponentModel.DataAnnotations;

namespace Clearance.Shared
{
    public class CollageDirectionDTO
    {
        public int Id { get; set; }
        public int CollageId { get; set; }
        [Required(ErrorMessage ="الرجاء إختيار الجهة")]
        public int DirectionId { get; set; }
        public string DirectionName { get; set; } = string.Empty;
    }
}
