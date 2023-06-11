using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        //public string Name { get; set; } = string.Empty;
        //public int? JobTitle_Id { get; set; }
        //public string Mobile { get; set; } = string.Empty;
        //public string PicURL { get; set; } = string.Empty;
        //[ForeignKey("JobTitle_Id")]
        //public JobTitle JobTitles { get; set; }
    }
}
