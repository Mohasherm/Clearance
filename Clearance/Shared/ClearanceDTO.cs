using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Clearance.Shared
{
    public  class ClearanceDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="الرجاء إدخال اسم الطالب")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال كنية الطالب")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال اسم الأب")]
        public string Father { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال اسم الأم")]
        public string Mother { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال الرقم الجامعي")]
        [MinLength(3 ,ErrorMessage ="لا يمكن ان يكون الرقم الجامعي أقل من 3 خانات")]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "الرقم الجامعي غير صالح يجب أن يحتوي أرقام فقط ")]
        public string UnivNum { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال الرقم الوطني")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "الرقم الوطني غير صالح يجب أن يحتوي أرقام فقط ويتكون من 11 خانة")]
        public string NationalNum { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار المركز")]
        public int? CollageId { get; set; }
        public string CollageName { get; set; } = string.Empty;
        [Required(ErrorMessage ="الرجاء إختيار القسم")]
        public int? DepartmentId { get; set; } 
        public string Department { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء إدخال رقم الموبايل")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(09[0-9]{8})$", ErrorMessage = "رقم الموبايل غير صالح يجب أن يحنوي أرقام فقط ويبدأ بالرقم 09 ويتكون من 10 خانات")]
        public string Mobile { get; set; }
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = "الرجاء إختيار الموظف المسؤول")]
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
        public bool Done{ get; set; }
        [Required(ErrorMessage ="اختر السنة الدراسية")]
        //[MinLength(4, ErrorMessage = "أدخل سنة صحيحة"), MaxLength(4, ErrorMessage = "أدخل سنة صحيحة")]
       // [Range(2020, 3000,ErrorMessage ="مجال السنوات يجب أن يكون أكبر من 2020")]
        public string Year { get; set; } = string.Empty;
        public DateTime? DateValidation { get; set; }
    }
}
