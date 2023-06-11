using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Clearance.Server.Repo
{
    public class ClaimsService : IClaimsService
    {
        private readonly UserManager<AppUser> _userManager;

        public ClaimsService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(AppUser user)
        {
            List<Claim> userClaims = new()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return userClaims;
        }
    }
}
