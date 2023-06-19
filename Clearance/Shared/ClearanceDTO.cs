using System.ComponentModel.DataAnnotations;

namespace Clearance.Shared
{
    public  class ClearanceDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Father { get; set; } = string.Empty;
        [Required]
        public string Mother { get; set; } = string.Empty;
        [Required]
        public int UnivNum { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NationalNum { get; set; }
        [Required]
        public int? CollageId { get; set; }
        public string CollageName { get; set; } = string.Empty;
        [Required] 
        public string Department { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(09[0-9]{8})$", ErrorMessage = "رقم الموبايل غير صالح")]
        public string Mobile { get; set; }
        public DateTime AppointmentDate { get; set; }
        [Required]
        public Guid? UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime OrderApplyDate { get; set; }
        /// <summary> state
        /// 1- بريء الذمة
        /// 2- غير بريء الذمة
        /// 3- قيد المعالجة
        /// </summary>
        public string State { get; set; } = string.Empty; 
        public DateTime? OrderRecieveDate { get; set; }
    }
}
