using Clearance.Shared;

namespace Clearance.Client.Authentication
{
    public interface ITokenService
    {
        Task<TokenDTO> GetToken();
        Task RemoveToken();
        Task SetToken(TokenDTO tokenDTO);
    }
}
