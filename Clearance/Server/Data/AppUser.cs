using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearance.Server.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Father { get; set; } = string.Empty;
        public int? Direction_Id { get; set; }
        [ForeignKey(nameof(Direction_Id))]
        public Direction Direction { get; set; }
    }
}
