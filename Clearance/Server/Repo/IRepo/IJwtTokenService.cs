using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Clearance.Server.Repo.IRepo
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GetJwtToken(List<Claim> userClaims);
    }
}
