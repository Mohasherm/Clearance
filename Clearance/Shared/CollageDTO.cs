using System.ComponentModel.DataAnnotations;

namespace Clearance.Shared
{
    public class CollageDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال اسم المركز")]
        public string Name { get; set; }
     
        [Required(ErrorMessage = "يرجى اختيار مسؤول المركز")]
        public Guid? UserId { get; set; }


        public string? UserName { get; set; }
    }
}
