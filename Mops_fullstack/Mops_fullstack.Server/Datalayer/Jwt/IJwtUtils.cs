using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Player player);

        public int? ValidatePlayerJwtToken(string token);
    }
}