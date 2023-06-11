using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearance.Shared
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
