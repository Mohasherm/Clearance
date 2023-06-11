using Clearance.Server.Data;
using System.Security.Claims;

namespace Clearance.Server.Repo.IRepo
{
    public interface IClaimsService
    {
        Task<List<Claim>> GetUserClaimsAsync(AppUser user);
    }

}
