using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Father { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DirectionName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int? Direction_Id { get; set; }
    }
}
