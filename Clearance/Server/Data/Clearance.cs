using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class Clearance
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string UnivNum { get; set; }
        public string NationalNum { get; set; }
        public int? CollageId { get; set; }
        public string Department { get; set; }
        public string Mobile { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Guid? UserId { get; set; }
        public DateTime OrderApplyDate { get; set; }
        public string State { get; set; }
        public DateTime? OrderRecieveDate { get; set; }
        public bool Done { get; set; } = false;

        [ForeignKey(nameof(UserId))]
        public AppUser AppUser { get; set; }
        [ForeignKey(nameof(CollageId))]
        public Collage Collage{ get; set; }

    }
}
